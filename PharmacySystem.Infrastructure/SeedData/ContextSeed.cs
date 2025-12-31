using Microsoft.AspNetCore.Identity;
using PharmacySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Infrastructure.SeedData
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
                await roleManager.CreateAsync(new IdentityRole(AppRole.Pharmacist));
                await roleManager.CreateAsync(new IdentityRole(AppRole.Storekeeper));
            }
        }
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager) 
        {
            var adminUser = new ApplicationUser
            {
                UserName = "maro",
                Email = "marawanmokhtar10@gmail.com",
                firstname = "Marwan", 
                lastname = "Mokhtar",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminUser, "Admin@123");
                    await userManager.AddToRoleAsync(adminUser, AppRole.Admin);
                }
            }
        }
    }
}
