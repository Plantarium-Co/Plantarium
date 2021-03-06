﻿// -----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Plantarium.Service.Common.Models;

    /// <summary>
    /// The base controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="response">The service response.</param>
        /// <returns>The response.</returns>
        public virtual IActionResult CreateResponse(ServiceResponse response)
        {
            if (!response.Status.Success)
            {
                return this.BadRequest(response.Status.Error);
            }

            return this.Ok();
        }

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <typeparam name="T">The service response type.</typeparam>
        /// <param name="response">The service response.</param>
        /// <returns>The response with data.</returns>
        public virtual IActionResult CreateResponse<T>(ServiceResponse<T> response) where T : Response, new()
        {
            if (!response.Status.Success)
            {
                return this.BadRequest(response.Status.Error);
            }

            return this.Ok(response.Data);
        }
    }
}