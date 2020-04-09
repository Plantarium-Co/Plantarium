// -----------------------------------------------------------------------
// <copyright file="LoginRequest.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Identity.Models.Login
{
    /// <summary>
    /// The login request.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Identity.Models.Request" />
    public class LoginRequest : Request
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
    }
}
