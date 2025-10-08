using api.Middleware;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace api.Test.Middleware
{
    public class ExceptionHandlingMiddlewareTests
    {
        [Fact]
        public async Task Invoke_ShouldReturn500AndJson_WhenExceptionIsThrown()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            context.Request.Path = "/test-path";

            // Simula un pipeline que lanza una excepción
            RequestDelegate next = _ => throw new InvalidOperationException("Simulated error");

            var middleware = new ExceptionHandlingMiddleware(next, loggerMock.Object);

            // Act
            await middleware.Invoke(context);

            // Assert
            context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            context.Response.ContentType.Should().Be("application/json");

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

            responseBody.Should().Contain("Ocurrió un error inesperado");
            responseBody.Should().Contain("Simulated error");
            responseBody.Should().Contain("/test-path");

            // Verifica que se haya logueado el error
            loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Unhandled exception occurred")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }

}