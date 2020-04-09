// -----------------------------------------------------------------------
// <copyright file="IIdentityService.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Identity
{
    using System.Threading.Tasks;
    using Plantarium.Service.Identity.Models.Login;
    using Plantarium.Service.Identity.Models.Register;

    /// <summary>
    /// The identity service.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<LoginResponse> Login(LoginRequest request);

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}