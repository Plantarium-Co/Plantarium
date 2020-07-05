// -----------------------------------------------------------------------
// <copyright file="HttpRequestExtensions.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Api.Extensions
{
    using System;
    using System.Net.Http.Headers;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Plantarium.Service.User.Models.Login;

    /// <summary>
    /// The http request extensions.
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Parses the authentication header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The login request.</returns>
        public static LoginRequest ParseAuthHeader(this HttpRequest request)
        {
            var result = new LoginRequest();

            if (request.Headers.ContainsKey("Authorization"))
            {
                var authHeader = request.Headers["Authorization"];
                var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
                var credentialBytes = Convert.FromBase64String(authHeaderValue.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                result.Username = credentials[0];
                result.Password = credentials[1];
            }

            return result;
        }
    }
}
