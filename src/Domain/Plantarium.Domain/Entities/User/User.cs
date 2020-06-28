// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Domain.Entities.User
{
    using System;

    /// <summary>
    /// The user entity.
    /// </summary>
    /// <seealso cref="Plantarium.Domain.Entities.Auditable" />
    public class User : Auditable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identity identifier.
        /// </summary>
        /// <value>
        /// The identity identifier.
        /// </value>
        public Guid IdentityId { get; set; }

        /// <summary>
        /// Gets or sets the name of the given.
        /// </summary>
        /// <value>
        /// The name of the given.
        /// </value>
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
    }
}
