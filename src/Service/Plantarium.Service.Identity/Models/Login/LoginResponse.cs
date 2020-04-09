// -----------------------------------------------------------------------
// <copyright file="LoginResponse.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Identity.Models.Login
{
    /// <summary>
    /// The login response.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Identity.Models.Response" />
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
