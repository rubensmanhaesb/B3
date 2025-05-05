using B3.API.Filters;

namespace B3.API.Extensions
{

    internal static class APIExtension
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.Filters.Add<ModelStateValidationFilter>();
            });


            return services;

        }
    }
}
