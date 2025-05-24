using B3.Application.Interfaces;
using B3.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace B3.Application.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<ICalculadoraCdbApplicationService, CalculadoraCdbApplicationService>();

            return services;
        }
    }
}
