using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MacroTracker.Models
{
    public class ConfigureIdentity
    {
        public static async Task CreateAdminUserAsync(IServiceProvider provider, ILogger logger)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<User>>();

            string username = "admin";
            string email = "admin@example.com"; 
            string password = "Admin123!";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    logger.LogError("Failed to create role: {0}", roleName);
                    return;
                }
            }

            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username, Email = email };
                var userResult = await userManager.CreateAsync(user, password);
                if (userResult.Succeeded)
                {
                    var roleAssignResult = await userManager.AddToRoleAsync(user, roleName);
                    if (!roleAssignResult.Succeeded)
                    {
                        logger.LogError("Failed to add user to role: {0}", roleName);
                    }
                }
                else
                {
                    logger.LogError("User creation failed: {0}", userResult.Errors.FirstOrDefault()?.Description);
                }
            }
        }
    }
}
