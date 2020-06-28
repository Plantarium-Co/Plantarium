// -----------------------------------------------------------------------
// <copyright file="RegisterExtensions.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Extensions
{
    using System;
    using Plantarium.Domain.Entities.User;
    using Plantarium.Service.User.Models.Register;

    /// <summary>
    /// The register extensions.
    /// </summary>
    public static class RegisterExtensions
    {
        /// <summary>
        /// Converts to user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="identityId">The identity identifier.</param>
        /// <returns>The user.</returns>
        public static User ToUser(this RegisterRequest request, Guid identityId)
        {
            return new User
            {
                IdentityId = identityId,
                GivenName = request.GivenName,
                LastName = request.LastName
            };
        }
    }
}
