using B3.Domain.Interfaces;
using B3.Domain.Services;
using B3.Domain.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace B3.Domain.Extensions
{
    public static class DomainServicesExtension
    {
        public static  IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICalculadoraCdbDomainService, CalculoCdbDomainService>();
            services.AddScoped<ITabelaImpostoService, TabelaImpostoService>();
            services.AddScoped<IValidator<(decimal, int)>, CdbCalculoValidation>();

            return services;
        }
    }
}
