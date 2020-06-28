// -----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User
{
    using System.Threading.Tasks;
    using Plantarium.Service.User.Models.Login;
    using Plantarium.Service.User.Models.Register;

    /// <summary>
    /// The user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The login response.</returns>
        Task<LoginResponse> Login(LoginRequest request);

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The register response.</returns>
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}