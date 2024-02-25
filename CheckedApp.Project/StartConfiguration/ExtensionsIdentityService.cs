using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.DATA;
using CheckedAppProject.LOGIC.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.API.StartConfiguration;

public static class ExtensionIdentityService
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserItemContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CheckedAppDbConnection")),
            ServiceLifetime.Transient
        );

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<UserItemContext>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await DbInitializer.InitializeAsync(userManager, roleManager);
        }
    }
}

