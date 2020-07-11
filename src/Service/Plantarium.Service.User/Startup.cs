// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User
{
    using Microsoft.Extensions.DependencyInjection;
    using Plantarium.Service.User.Repositories;
    using Plantarium.Service.User.Repositories.Interfaces;

    /// <summary>
    /// The user service startup.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Adds the user service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
