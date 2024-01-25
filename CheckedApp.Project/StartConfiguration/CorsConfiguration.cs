namespace CheckedAppProject.API.StartConfiguration
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(WebApplicationBuilder builder)
        {
            var frontendURL = builder.Configuration.GetValue<string>("http://localhost:3000");
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
