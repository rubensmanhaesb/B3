using B3.Application.Services;
using B3.Domain.Entities;
using B3.Domain.Interfaces;
using Moq;

namespace ApplicationTests
{
    public class CalculadoraCdbApplicationServiceTests
    {
        [Fact]
        public async Task Deve_Retornar_Dto_Corretamente_Com_Dados_Do_Dominio()
        {
            var valorInicial = 1000m;
            var prazo = 6;
            var cdbCalculado = new Cdb
            {
                ValorInicial = valorInicial,
                PrazoMeses = prazo,
                ValorBruto = 1050.00m,
                ValorLiquido = 1040.00m
            };

            var mockDomainService = new Mock<ICalculadoraCdbDomainService>();
            mockDomainService
                .Setup(x => x.Calcular(valorInicial, prazo))
                .Returns(cdbCalculado);

            var appService = new CalculadoraCdbApplicationService(mockDomainService.Object);

            var resultado = await appService.CalcularAsync(valorInicial, prazo, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(cdbCalculado.ValorInicial, resultado.ValorInicial);
            Assert.Equal(cdbCalculado.PrazoMeses, resultado.PrazoMeses);
            Assert.Equal(cdbCalculado.ValorBruto, resultado.ValorBruto);
            Assert.Equal(cdbCalculado.ValorLiquido, resultado.ValorLiquido);
        }
    }
}
