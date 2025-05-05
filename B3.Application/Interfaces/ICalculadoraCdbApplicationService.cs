using B3.Application.Dtos;



namespace B3.Application.Interfaces
{
    public interface ICalculadoraCdbApplicationService
    {
        Task<CdbResultDto> CalcularAsync(decimal valorInicial, int prazoEmMeses, CancellationToken cancellationToken);
    }
}
