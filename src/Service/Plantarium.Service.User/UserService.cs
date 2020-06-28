// -----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User
{
    using System;
    using System.Threading.Tasks;
    using Plantarium.Infrastructure.Exceptions;
    using Plantarium.Infrastructure.Wrappers.Interfaces;
    using Plantarium.Service.User.Exceptions;
    using Plantarium.Service.User.Extensions;
    using Plantarium.Service.User.Models.Login;
    using Plantarium.Service.User.Models.Register;
    using Plantarium.Service.User.Repositories.Interfaces;

    /// <summary>
    /// The user service.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Identity.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The identity wrapper
        /// </summary>
        private readonly IIdentityWrapper identityWrapper;

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="identityWrapper">The identity wrapper.</param>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IIdentityWrapper identityWrapper, IUserRepository userRepository)
        {
            this.identityWrapper = identityWrapper;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The register response.</returns>
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var result = new RegisterResponse();

            try
            {
                var identityId = await this.identityWrapper.RegisterAsync(request.Username, request.Password);
                await this.identityWrapper.AddToRoleAsync(request.Username, request.Role);
                await this.userRepository.CreateUserAsync(request.ToUser(identityId));
            }
            catch (IdentityException ex)
            {
                result.Errors.AddRange(ex.Errors);
            }
            catch (UserServiceDataException ex)
            {
                result.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The login response.</returns>
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = new LoginResponse();

            try
            {
                result.Token = await this.identityWrapper.AuthenticateAsync(request.Username, request.Password);
            }
            catch (IdentityException ex)
            {
                result.Errors.AddRange(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new UserServiceException(ex.Message, ex);
            }

            return result;
        }
    }
}
