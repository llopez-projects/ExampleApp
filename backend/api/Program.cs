using api.Middleware;
using application.DTOs;
using application.Helpers.Config;
using application.Interfaces.Repositories;
using application.Interfaces.Services;
using application.Mapping;
using application.Services;
using application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using infrastructure.Logging;
using infrastructure.Persistence;
using infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

//Logueo de errores internos de Elastic
Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"[Serilog] {msg}"));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation(); 

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IValidator<CreateEmployeeDto>, CreateEmployeeDtoValidator>();

builder.Services.Configure<ElasticLoggingSettings>(builder.Configuration.GetSection("ElasticLogging"));

var elasticSettings = builder.Configuration
    .GetSection("ElasticLogging")
    .Get<ElasticLoggingSettings>();

var loggerConfig = new LoggerConfiguration()
    .Enrich.FromLogContext();

if (builder.Environment.IsDevelopment())
{
    loggerConfig.WriteTo.Console();
}

if (elasticSettings.Enabled)
{
    loggerConfig.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSettings.Uri))
    {
        AutoRegisterTemplate = true,
        IndexFormat = elasticSettings.IndexFormat,
        CustomFormatter = new JsonFormatter()
    });
}

Log.Logger = loggerConfig.CreateLogger();
builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<EndpointLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
