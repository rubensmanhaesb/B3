using B3.Application.Dtos;
using B3.Application.Interfaces;
using B3.Domain.Interfaces;


namespace B3.Application.Services
{
    public class CalculadoraCdbApplicationService(ICalculadoraCdbDomainService calculadoraService) : ICalculadoraCdbApplicationService
    {
        private readonly ICalculadoraCdbDomainService _calculadoraDomainService = calculadoraService;


        public Task<CdbResultDto> CalcularAsync(decimal valorInicial, int prazoEmMeses, CancellationToken cancellationToken)
        {
            var resultado = _calculadoraDomainService.Calcular(valorInicial, prazoEmMeses);

            var resultDto = new CdbResultDto
            {
                ValorInicial = resultado.ValorInicial,
                PrazoMeses = resultado.PrazoMeses,
                ValorBruto = resultado.ValorBruto,
                ValorLiquido = resultado.ValorLiquido
            };

            return Task.FromResult(resultDto);
        }
    }
}
