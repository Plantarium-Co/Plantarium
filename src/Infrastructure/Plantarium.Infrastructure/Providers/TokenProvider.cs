// -----------------------------------------------------------------------
// <copyright file="TokenProvider.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Providers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Plantarium.Infrastructure.Configurations;
    using Plantarium.Infrastructure.Providers.Interfaces;

    /// <summary>
    /// The token provider.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Providers.Interfaces.ITokenProvider" />
    public class TokenProvider : ITokenProvider
    {
        /// <summary>
        /// The JWT settings.
        /// </summary>
        private readonly JwtSettings jwtSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenProvider"/> class.
        /// </summary>
        /// <param name="optionsMonitor">The options monitor.</param>
        public TokenProvider(IOptionsMonitor<JwtSettings> optionsMonitor)
        {
            this.jwtSettings = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns>
        /// The generated token.
        /// </returns>
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var result = string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(this.jwtSettings.ExpiryInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), this.jwtSettings.Algorithm)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            result = tokenHandler.WriteToken(token);

            return result;
        }
    }
}
