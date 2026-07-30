using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFinalProject.Application.Constants;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Seed
{
    public static class SeedData
    {
        public static async Task SeedAsync(
            FinalDbContext db,
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<User> userManager)
        {
            await db.Database.MigrateAsync();

            await EnsureRolesAsync(roleManager);
            await EnsureAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            var roles = new[]
            {
               RoleConstants.JobSeekerRole,
               RoleConstants.EmployerRole,
               RoleConstants.AdminRole
            };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Creating Role {roleName} was not successful :" +
                            string.Join(" | ", result.Errors.Select(x => x.Description)));
                    }
                }
            }
        }

        private static async Task EnsureAdminAsync(UserManager<User> userManager)
        {
            var adminEmail = "admin@gmail.com";
            var adminUserName = "System_Admin";
            var adminPassword = "Admin@123456";

            var existingEmail = await userManager.FindByEmailAsync(adminEmail);
            if (existingEmail != null)
                return;

            var existingUsername = await userManager.FindByNameAsync(adminUserName);
            if (existingUsername != null)
                return;

            var admin = new User("System", "Admin", "9876543210", adminEmail)
            {
                Id = Guid.NewGuid(),
                UserName = adminUserName,
                EmailConfirmed = true,
                IsApproved = true,
                Role = UserRole.Admin
            };

            var createResult = await userManager.CreateAsync(admin, adminPassword);
            if (!createResult.Succeeded)
            {
                throw new Exception("Creating Admin User Failed : " +
                    string.Join(" | ", createResult.Errors.Select(x => x.Description)));
            }

            var roleResult = await userManager.AddToRoleAsync(admin, RoleConstants.AdminRole);
            if (!roleResult.Succeeded)
            {
                throw new Exception("Add Role To Admin User Failed !");
            }
        }
    }
}
