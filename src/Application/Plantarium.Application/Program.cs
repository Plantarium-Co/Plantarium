// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Plantarium Co.">
//     Plantarium, All rights reserved
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Application
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The execution program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The web host builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
