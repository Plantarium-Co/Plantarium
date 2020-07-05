// -----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Plantarium.Api.Extensions;
    using Plantarium.Service.User;
    using Plantarium.Service.User.Models.Register;

    /// <summary>
    /// The user controller.
    /// </summary>
    /// <seealso cref="Plantarium.Api.Controllers.BaseController" />
    [Authorize]
    public class UserController : BaseController
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The register response.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            var response = await this.userService.Register(request);
            return this.CreateResponse(response);
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>The login response.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var response = await this.userService.Login(this.Request.ParseAuthHeader());
            return this.CreateResponse(response);
        }
    }
}