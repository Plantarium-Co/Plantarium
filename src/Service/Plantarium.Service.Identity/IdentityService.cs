// -----------------------------------------------------------------------
// <copyright file="IdentityService.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Identity
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Plantarium.Infrastructure.Exceptions;
    using Plantarium.Infrastructure.Wrappers.Interfaces;
    using Plantarium.Service.Identity.Models.Login;
    using Plantarium.Service.Identity.Models.Register;

    /// <summary>
    /// The identity service.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Identity.IIdentityService" />
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// The identity wrapper
        /// </summary>
        private readonly IIdentityWrapper identityWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="identityWrapper">The identity wrapper.</param>
        public IdentityService(IIdentityWrapper identityWrapper)
        {
            this.identityWrapper = identityWrapper;
        }

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var result = new RegisterResponse();

            try
            {
                await this.identityWrapper.RegisterAsync(request.Username, request.Password);
                await this.identityWrapper.AddToRoleAsync(request.Username, request.Role);
            }
            catch (IdentityException ex)
            {
                result.Errors.AddRange(ex.Errors.Select(error => error.Value));
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = new LoginResponse();

            try
            {
                result.Token = await this.identityWrapper.AuthenticateAsync(request.Username, request.Password);
            }
            catch (IdentityException ex)
            {
                result.Errors.AddRange(ex.Errors.Select(error => error.Value));
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}
