// -----------------------------------------------------------------------
// <copyright file="LoginResponse.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Models.Login
{
    using Newtonsoft.Json;
    using Plantarium.Service.Common.Models;

    /// <summary>
    /// The login response.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Common.Models.Response" />
    public class LoginResponse : Response
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
