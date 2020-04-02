// -----------------------------------------------------------------------
// <copyright file="AuthException.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Providers.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// The token provider.
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns>The generated token.</returns>
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
