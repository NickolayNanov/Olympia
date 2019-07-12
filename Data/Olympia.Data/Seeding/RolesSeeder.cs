namespace Olympia.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Olympia.Common;
    using Olympia.Data.Domain;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(OlympiaDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<OlympiaUserRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<OlympiaUserRole> roleManager, string roleName)
        {
            var role = await roleManager.CreateAsync(new OlympiaUserRole(roleName));

            if (role == null)
            {
                var result = await roleManager.CreateAsync(new OlympiaUserRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
