// -----------------------------------------------------------------------
// <copyright file="RegisterRequest.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Identity.Models.Register
{
    using Plantarium.Data.Constants;

    /// <summary>
    /// The register request.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Identity.Models.Request" />
    public class RegisterRequest : Request
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Role Role { get; set; } = Role.User;
    }
}
