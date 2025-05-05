using B3.Domain.Entities;


namespace B3.Domain.Interfaces
{
    public interface ICalculadoraCdbDomainService
    {
        Cdb Calcular(decimal valorInicial, int prazoMeses);
    }

}
