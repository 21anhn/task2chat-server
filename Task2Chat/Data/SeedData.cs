using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Seed roles
        var roleNames = new List<string> { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                });
            }
        }

        // Seed admin user
        var adminEmail = "task2chatAdmin@gmail.com";
        var adminPassword = "Admin123@!";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
            };

            await userManager.CreateAsync(admin, adminPassword);
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
