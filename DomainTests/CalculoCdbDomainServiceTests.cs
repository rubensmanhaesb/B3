using B3.Domain.Services;
using Moq;
using B3.Domain.Interfaces;
using B3.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using B3.Domain.Validations;

namespace DomainTests
{

    public class CalculoCdbDomainServiceTests
    {
        [Fact]
        public void Deve_Calcular_Cdb_Com_Sucesso()
        {
            // Arrange
            decimal valorInicial = 1000;
            int prazoMeses = 6;
            decimal aliquotaSimulada = 0.20m;

            var mockTabelaImposto = new Mock<ITabelaImpostoService>();
            mockTabelaImposto.Setup(x => x.ObterAliquota(prazoMeses)).Returns(aliquotaSimulada);

            var mockValidator = new Mock<IValidator<(decimal, int)>>();
            mockValidator
                .Setup(v => v.Validate(It.IsAny<(decimal, int)>()))
                .Returns(new ValidationResult()); // Simula validação bem-sucedida

            var service = new CalculoCdbDomainService(mockTabelaImposto.Object, mockValidator.Object);

            // Act
            var resultado = service.Calcular(valorInicial, prazoMeses);

            // Assert
            Assert.Equal(valorInicial, resultado.ValorInicial);
            Assert.Equal(prazoMeses, resultado.PrazoMeses);
            Assert.True(resultado.ValorBruto > valorInicial);
            Assert.True(resultado.ValorLiquido > 0);
            Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
        }


        [Theory]
        [InlineData(0, 6)]
        [InlineData(-100, 12)]
        [InlineData(1000, 0)]
        [InlineData(1000, 1)]
        public void Deve_Lancar_ValidationException_Para_Parametros_Invalidos(decimal valorInicial, int prazoMeses)
        {

            var mockImposto = new Mock<ITabelaImpostoService>();
            mockImposto.Setup(x => x.ObterAliquota(It.IsAny<int>())).Returns(0.20m);

            var validator = new CdbCalculoValidation();
            var service = new CalculoCdbDomainService(mockImposto.Object, validator);


            Assert.Throws<ValidationException>(() =>
                service.Calcular(valorInicial, prazoMeses));
        }
    }

}
