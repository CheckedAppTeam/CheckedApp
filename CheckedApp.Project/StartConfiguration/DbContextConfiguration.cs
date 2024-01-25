using CheckedAppProject.DATA;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.API.StartConfiguration
{
    public static class DbContextConfiguration
    {
        public static void ConfigureDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<UserItemContext>(
                option => option.UseNpgsql(builder.Configuration["CheckedAppDbConnection"]),
                ServiceLifetime.Transient
            );
        }
        public static void ConfigureIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
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

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
        }
        public static async void InitializeDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await DbInitializer.InitializeAsync(userManager, roleManager);
            }
        }
    }
}
