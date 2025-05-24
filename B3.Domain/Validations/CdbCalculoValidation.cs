using FluentValidation;

namespace B3.Domain.Validations
{
    public class CdbCalculoValidation : AbstractValidator<(decimal valorInicial, int prazoMeses)>
    {
        public CdbCalculoValidation()
        {
            RuleFor(x => x.valorInicial)
                .GreaterThan(0)
                .WithMessage("Valor inicial deve ser maior que zero.");

            RuleFor(x => x.prazoMeses)
                .GreaterThan(1)
                .WithMessage("Prazo deve ser maior que 1 mês.");
        }
    }
}
