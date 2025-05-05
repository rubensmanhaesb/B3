using B3.Domain.Entities;
using B3.Domain.Interfaces;
using FluentValidation;


namespace B3.Domain.Services
{
    public class CalculoCdbDomainService(
        ITabelaImpostoService tabelaImpostoService,
         IValidator<(decimal, int)> validator) : ICalculadoraCdbDomainService
    {
        private const decimal CDI = 0.009m; // 0,9% ao mês
        private const decimal TB = 1.08m;   // 108% do CDI
        private readonly ITabelaImpostoService _tabelaImpostoService = tabelaImpostoService;
        private readonly IValidator<(decimal, int)> _validator = validator;


        public  Cdb Calcular(decimal valorInicial, int prazoMeses)
        {
            var valor = valorInicial;

            _validator.ValidateAndThrow((valorInicial, prazoMeses));

            for (int i = 0; i < prazoMeses; i++)
            {
                valor *= (1 + (CDI * TB));
            }

            var valorBruto = Math.Round(valor, 2);
            var lucro = valorBruto - valorInicial;
            var aliquota = _tabelaImpostoService.ObterAliquota(prazoMeses);
            var imposto = Math.Round(lucro * aliquota, 2);
            var valorLiquido = valorBruto - imposto;

            return new Cdb
            {
                ValorInicial = valorInicial,
                PrazoMeses = prazoMeses,
                ValorBruto = valorBruto,
                ValorLiquido = valorLiquido
            };
        }
    }


}
