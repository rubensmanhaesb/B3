using B3.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;


namespace APITests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove implementação real
                services.RemoveAll<ICalculadoraCdbApplicationService>();

                // Substitui por mock que lança exceção
                var mock = new Mock<ICalculadoraCdbApplicationService>();
                mock.Setup(m => m.CalcularAsync(It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new InvalidOperationException("Erro simulado"));

                services.AddSingleton(mock.Object);
            });
        }
    }
}
