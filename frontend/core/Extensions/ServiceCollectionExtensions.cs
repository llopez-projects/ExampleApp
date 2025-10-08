using core.Interfaces;
using core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration config)
    {
        var baseUrl = config["Api:BaseUrl"] ?? throw new InvalidOperationException("Api:BaseUrl no está configurado.");

        services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        });

        return services;
    }
}

