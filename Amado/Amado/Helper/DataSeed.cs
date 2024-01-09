using Amado.Entities;
using Microsoft.AspNetCore.Identity;

namespace Amado.Helper
{
    public class DataSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var user = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                };

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var existingUser = await userManager.FindByNameAsync("admin@gmail.com");
                if (existingUser != null) return;

                await userManager.CreateAsync(user, "hello12345");

                return;
            }
        }
    }
}
