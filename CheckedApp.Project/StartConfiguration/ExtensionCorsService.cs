namespace CheckedAppProject.API.StartConfiguration
{
    public static class ExtensionCorsService
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var frontendURL = configuration.GetValue<string>("AllowedOrigins:FrontendURL");

            if (!string.IsNullOrEmpty(frontendURL))
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder
                            .WithOrigins(frontendURL)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });
            }

            return services;
        }
    }
}
