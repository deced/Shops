using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shops.App.Models.Identity;
using Syncfusion.EJ2.Linq;

namespace Shops.App.Configuration
{
    public static class SeedConfiguration
    {
        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.Roles.Count() == 0)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("courier"));
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
        }
        
        public static async Task CreateUsers(UserManager<User> userManager)
        {
            if(userManager.Users.Count() == 0)
            {
                var admin = new User() { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
                var courier = new User() { Email = "courier@gmail.com", UserName = "courier@gmail.com" };
                var user = new User(){Email = "user@gmail.com", UserName = "user@gmail.com"};
                await userManager.CreateAsync(admin,"12345678Qq_"); 
                await userManager.CreateAsync(courier,"12345678Qq_"); 
                await userManager.CreateAsync(user,"12345678Qq_");

                await userManager.AddToRoleAsync(admin, "admin");
                await userManager.AddToRoleAsync(courier, "courier");
                await userManager.AddToRoleAsync(user, "user");
            }
        }
    }
}