// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The data startup.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    assemblyOptions => assemblyOptions.MigrationsAssembly("Plantarium.Data")));
        }
    }
}
