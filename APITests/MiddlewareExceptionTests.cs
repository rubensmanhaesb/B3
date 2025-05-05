using B3.API.Middlewares;
using B3.API.Request;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http.Json;


namespace APITests
{
    public class MiddlewareExceptionTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();


        [Fact]
        public async Task Post_Calcular_Deve_Retornar_500_Se_ApplicationService_Lancar_Erro()
        {
            var request = new CdbRequest
            {
                ValorInicial = 1000,
                PrazoEmMeses = 12
            };

            var response = await _client.PostAsJsonAsync("/api/CalculadoraCdb", request);
            var body = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
            body.Should().Contain("Erro interno no servidor");
        }

        [Fact]
        public async Task Middleware_Deve_Passar_Adiante_Sem_Excecao()
        {
            var context = new DefaultHttpContext();
            var nextMock = new Mock<RequestDelegate>();
            nextMock.Setup(n => n(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            var logger = Mock.Of<ILogger<ExceptionMiddleware>>();

            var envMock = new Mock<IHostEnvironment>();
            envMock.Setup(e => e.EnvironmentName).Returns("Development");

            var middleware = new ExceptionMiddleware(nextMock.Object, logger, envMock.Object);

            await middleware.InvokeAsync(context);

            nextMock.Verify(n => n(It.IsAny<HttpContext>()), Times.Once);
        }

        [Fact]
        public async Task ExceptionMiddleware_Deve_Propagar_Excecao_Critica()
        {
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.Configure(app =>
                    {
                        app.UseMiddleware<ExceptionMiddleware>();
                        app.Run(_ => throw new OutOfMemoryException("Erro crítico")); // deve escapar
                    });
                });

            var client = app.CreateClient();

            Func<Task> act = async () => await client.GetAsync("/");
            await act.Should().ThrowAsync<OutOfMemoryException>(); // não é capturada
        }

        [Fact]
        public async Task Middleware_Deve_Retornar_Erro_Padrao_Quando_Nao_Desenvolvimento()
        {
            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Production"); // força IsDevelopment() == false
                    builder.Configure(app =>
                    {
                        app.UseMiddleware<ExceptionMiddleware>();
                        app.Run(_ => throw new Exception("Erro genérico"));
                    });
                });

            var client = factory.CreateClient();
            var response = await client.GetAsync("/");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("Ocorreu um erro inesperado");
        }

    }

}
