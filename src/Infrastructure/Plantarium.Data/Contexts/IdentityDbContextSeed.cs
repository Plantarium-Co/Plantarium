// -----------------------------------------------------------------------
// <copyright file="IdentityDbContextSeed.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Data.Contexts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Plantarium.Domain.Constants;

    /// <summary>
    /// The identity database context seed.
    /// </summary>
    public static class IdentityDbContextSeed
    {
        /// <summary>
        /// Seeds the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <returns>The seed task.</returns>
        public static async Task SeedAsync(IdentityDbContext context, RoleManager<IdentityRole<Guid>> roleManager)
        {
            using (context)
            {
                var migrations = await context.Database.GetPendingMigrationsAsync();

                if (migrations.Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Roles.Any())
                {
                    _ = await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Administrator.ToString()));
                    _ = await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Editor.ToString()));
                    _ = await roleManager.CreateAsync(new IdentityRole<Guid>(Role.User.ToString()));
                }
            }
        }
    }
}
