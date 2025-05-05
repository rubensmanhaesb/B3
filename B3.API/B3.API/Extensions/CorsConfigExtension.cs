namespace B3.API.Extensions
{
    public static class CorsConfigExtension
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration configuration)
        {

            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));


            var origins = configuration.GetSection("CorsConfigSettings:Origins").Get<string[]>()
                ?? throw new InvalidOperationException("CORS origins configuration is missing.");

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    builder.WithOrigins(origins)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors("DefaultPolicy");
            return app;
        }
    }
}
