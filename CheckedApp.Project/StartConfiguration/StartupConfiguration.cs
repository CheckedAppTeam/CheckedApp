using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CheckedAppProject.API.StartConfiguration
{
    public class StartupConfiguration
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAppServices();
            builder.Services.AddAppRepositories();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
            builder.Services.ConfigureCors(builder.Configuration);
            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureIdentity();
        }
        public static void ConfigureApp(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }        
    }
}
