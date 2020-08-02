// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Plantarium.Infrastructure.Configurations;
    using Plantarium.Infrastructure.Factories;
    using Plantarium.Infrastructure.Factories.Interfaces;
    using Plantarium.Infrastructure.Helpers;
    using Plantarium.Infrastructure.Helpers.Interfaces;
    using Plantarium.Infrastructure.Providers;
    using Plantarium.Infrastructure.Providers.Interfaces;
    using Plantarium.Infrastructure.Wrappers;
    using Plantarium.Infrastructure.Wrappers.Interfaces;

    /// <summary>
    /// The infrastructure startup.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddIdentity(services, configuration);
            AddAuthentication(services, configuration);
            AddDatabase(services, configuration);
        }

        /// <summary>
        /// Adds the identity.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IIdentityWrapper, IdentityWrapper>();
            services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));
        }

        /// <summary>
        /// Adds the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection(nameof(JwtSettings));
            var secret = jwtSection.GetValue<string>(nameof(JwtSettings.Secret));

            services.AddAuthentication(authOpts =>
            {
                authOpts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOpts =>
            {
                jwtOpts.RequireHttpsMetadata = false;
                jwtOpts.SaveToken = true;
                jwtOpts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<JwtSettings>(jwtSection);
        }

        /// <summary>
        /// Adds the database.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITransactionScopeFactory, TransactionScopeFactory>();
            services.AddSingleton<IDbCommandBuilderFactory, DbCommandBuilderFactory>();
            services.AddScoped<IDbConnectionFactory>(_ => new DbConnectionFactory(configuration.GetConnectionString("Database")));
            services.AddScoped<IDbHelper, SqlHelper>();
        }
    }
}
