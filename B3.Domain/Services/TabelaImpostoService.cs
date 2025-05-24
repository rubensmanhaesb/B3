using B3.Domain.Interfaces;

namespace B3.Domain.Services
{
    public class TabelaImpostoService : ITabelaImpostoService
    {
        private static readonly SortedDictionary<int, decimal> _faixasImposto = new()
        {
            { 6,  0.225m },           // até 6 meses
            { 12, 0.20m },            // até 12 meses
            { 24, 0.175m },           // até 24 meses
            { int.MaxValue, 0.15m }   // acima de 24 meses
        };

        public decimal ObterAliquota(int prazoMeses)
        {
            return _faixasImposto.First(x => prazoMeses <= x.Key).Value;
        }
    }
}
