using infrastructure.Logging;
using infrastructure.Logging.Models;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Options;
using Serilog;


namespace api.Middleware
{
    public class EndpointLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ElasticLoggingSettings _settings;

        public EndpointLoggingMiddleware(RequestDelegate next, IOptions<ElasticLoggingSettings> settings)
        {
            _next = next;
            _settings = settings.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            //TODO: Revisar el uso de atributos en los métodos
            if (!_settings.Enabled || path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            // Capturar datos de la request
            var request = context.Request;
            request.EnableBuffering();

            var requestBody = await ReadStreamAsync(request.Body);
            request.Body.Position = 0;

            var originalResponseBody = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);

            stopwatch.Stop();

            // Capturar datos de la response
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalResponseBody);

            var endpointLog = new EndpointLog
            {
                Timestamp = DateTime.UtcNow,
                Module = InferModule(request.Path),
                HttpMethod = request.Method,
                Route = request.Path,
                IP = context.Connection.RemoteIpAddress?.ToString(),
                StatusCode = context.Response.StatusCode,
                DurationMs = stopwatch.ElapsedMilliseconds,
                RequestSizeBytes = request.ContentLength ?? Encoding.UTF8.GetByteCount(requestBody),
                ResponseSizeBytes = Encoding.UTF8.GetByteCount(responseBody),
                QueryString = request.QueryString.ToString(),
                RequestBody = Truncate(requestBody, 1000),
                ResponseBody = Truncate(responseBody, 1000)
            };

            Log.Information("Endpoint invoked: {@EndpointLog}", endpointLog);
        }

        private static async Task<string> ReadStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
            return await reader.ReadToEndAsync();
        }

        private static string Truncate(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...";
        }

        private static string InferModule(PathString path)
        {
            if (path.StartsWithSegments("/api/employees")) return "Empleados";
            if (path.StartsWithSegments("/api/departments")) return "Departamentos";
            return "General";
        }
    }
}
