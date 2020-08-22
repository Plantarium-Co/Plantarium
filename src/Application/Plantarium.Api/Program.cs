// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Api
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Plantarium.Data.Contexts;
    using Plantarium.Infrastructure.Logging.Interfaces;
    using static Plantarium.Data.Contexts.IdentityDbContextSeed;

    /// <summary>
    /// The execution program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The main task.</returns>
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger>();

                try
                {
                    var context = services.GetRequiredService<IdentityDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                    await SeedAsync(context, roleManager);
                }
                catch (Exception exception)
                {
                    logger.Error(exception, "Role seeding failed");
                }
            }

            host.Run();
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The web host builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
