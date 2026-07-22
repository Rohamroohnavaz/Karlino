using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyFinalProject.Application.Constants;
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
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<FinalDbContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            await db.Database.EnsureCreatedAsync();

            string[] roles =
            [
                RoleConstants.JobSeekerRole,
                RoleConstants.EmployerRole,
                RoleConstants.AdminRole
            ];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
            }

            const string adminEmail = "admin@gmail.com";
            const string adminPassword = "admin8844_43";

            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                var admin = new User
                {
                    UserName = "System_Admin",
                    Email = adminEmail,
                    IsApproved = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, RoleConstants.AdminRole);
            }

            Console.WriteLine("Seed Data Is OK");
            Console.WriteLine($"Admin : {adminEmail} / {adminPassword}");
        }
    }
}
