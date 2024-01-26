namespace CheckedAppProject.API.StartConfiguration
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(WebApplicationBuilder builder)
        {
            var frontendURL = builder.Configuration.GetValue<string>("AllowedOrigins:FrontendURL");

            if (frontendURL != null ) { 
                builder.Services.AddCors(options =>
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
        }
    }
}
