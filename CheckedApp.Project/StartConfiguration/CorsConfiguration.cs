namespace CheckedAppProject.API.StartConfiguration
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(WebApplicationBuilder builder)
        {
            var frontendURL = builder.Configuration.GetValue<string>("AllowedOrigins:FrontendURL");

            if (!string.IsNullOrEmpty(frontendURL)) { 
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
