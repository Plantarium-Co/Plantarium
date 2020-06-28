// -----------------------------------------------------------------------
// <copyright file="LoginResponse.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Models.Login
{
    using Plantarium.Domain.Models;

    /// <summary>
    /// The login response.
    /// </summary>
    /// <seealso cref="Plantarium.Domain.Models.Response" />
    public class LoginResponse : Response
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }
    }
}
