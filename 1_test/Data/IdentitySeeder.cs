using Microsoft.AspNetCore.Identity;
using _1_test.Infrastructure;
using _1_test.Models;

namespace _1_test.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await EnsureRoleAsync(roleManager, AppRoles.Admin);
        await EnsureRoleAsync(roleManager, AppRoles.User);

        await EnsureAdminUserAsync(userManager);
        await EnsureDemoUserAsync(userManager);
    }

    private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task EnsureAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@dsr.local";
        const string adminPassword = "Admin123!";

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "DSR"
            };

            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }
        else
        {
            await EnsureUserRoleAsync(userManager, admin, AppRoles.Admin);
        }
    }

    private static async Task EnsureDemoUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string userEmail = "user@dsr.local";
        const string userPassword = "User123!";

        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail,
                FirstName = "Demo",
                LastName = "User"
            };

            var result = await userManager.CreateAsync(user, userPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, AppRoles.User);
            }
        }
        else
        {
            await EnsureUserRoleAsync(userManager, user, AppRoles.User);
        }
    }

    private static async Task EnsureUserRoleAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string roleName)
    {
        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
