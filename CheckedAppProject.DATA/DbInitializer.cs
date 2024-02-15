using CheckedAppProject.DATA.Entities;
using Microsoft.AspNetCore.Identity;

namespace CheckedAppProject.DATA
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            if (await userManager.FindByEmailAsync("admin@ham.com") == null)
            {
                var adminUser = new AppUser
                {
                    UserName = "Admin",
                    Email = "admin@ham.com"
                };
                var createAdminResult = await userManager.CreateAsync(adminUser, "klarowanieGigaKraBowe");
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }       
        }
    }
}

