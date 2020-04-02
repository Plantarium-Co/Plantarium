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
    using Microsoft.IdentityModel.Tokens;
    using Plantarium.Infrastructure.Providers.Interfaces;

    /// <summary>
    /// The token provider.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Providers.Interfaces.ITokenProvider" />
    public class TokenProvider : ITokenProvider
    {
        /// <summary>
        /// The secret.
        /// </summary>
        private const string SECRET = "Plantarium";

        /// <summary>
        /// The expiry in days.
        /// </summary>
        private const double EXPIRYDAYS = 7;

        /// <summary>
        /// The algorithm.
        /// </summary>
        private const string ALGORITHM = SecurityAlgorithms.HmacSha256Signature;

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
            var key = Encoding.ASCII.GetBytes(SECRET);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(EXPIRYDAYS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), ALGORITHM)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            result = tokenHandler.WriteToken(token);

            return result;
        }
    }
}
