using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebMovieApp_4_10.Authorization;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Data
{
    public class SeedData
    {

        public static async Task Initialize (IServiceProvider serviceProvider, string userEmail, string password, string firstName, string LastName)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var managreUid = await EnsureUser(serviceProvider, userEmail, password, firstName, LastName);
                await EnsureRole(serviceProvider, managreUid, Constraints.AdminRole);
            }

        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string userEmail, string initPw, string firstName, string LastName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(userEmail);

            if(user == null)
            {
                user = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    PasswordHash = initPw,
                    FirstName = firstName,
                    LastName = LastName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, initPw);
                await userManager.AddClaimAsync(user, new Claim("FullName", user.Fullname));
            }
            if(user == null)
            {
                throw new Exception("User did not get created!");
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string userId, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            IdentityResult ir;

            if(!await roleManager.RoleExistsAsync(role))
            {
                ir = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManger = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManger.FindByIdAsync(userId);

            if(user == null)
            {
                throw new Exception("User not existing");
            }

            ir = await userManger.AddToRoleAsync(user, role);

            return ir;
        }
    }
}
