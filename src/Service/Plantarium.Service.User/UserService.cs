// -----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User
{
    using System;
    using System.Threading.Tasks;
    using Plantarium.Infrastructure.Logging.Interfaces;
    using Plantarium.Infrastructure.Wrappers.Interfaces;
    using Plantarium.Service.Common.Exceptions;
    using Plantarium.Service.Common.Models;
    using Plantarium.Service.User.Extensions;
    using Plantarium.Service.User.Models.Login;
    using Plantarium.Service.User.Models.Register;
    using Plantarium.Service.User.Repositories.Interfaces;
    using static Plantarium.Service.Common.Exceptions.ExceptionManager;

    /// <summary>
    /// The user service.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Identity.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

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
        /// <param name="logger">The logger.</param>
        /// <param name="identityWrapper">The identity wrapper.</param>
        /// <param name="userRepository">The user repository.</param>
        public UserService(
            ILogger logger,
            IIdentityWrapper identityWrapper,
            IUserRepository userRepository)
        {
            this.logger = logger;
            this.identityWrapper = identityWrapper;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The service response.</returns>
        public async Task<ServiceResponse> Register(RegisterRequest request)
        {
            var response = new ServiceResponse();

            try
            {
                request.ValidateAndThrow(new RegisterRequestValidator());
                var identityId = await this.identityWrapper.RegisterAsync(request.Email, request.Username, request.Password);
                await this.identityWrapper.AddToRoleAsync(request.Username, request.Role);
                await this.userRepository.CreateUserAsync(request.ToUser(identityId));
            }
            catch (Exception exception) when (IsClientError(exception))
            {
                AddErrors(exception, response.Status);
                this.logger.Warning("{Error} encountered by {Username} on register", response.Status.Error.Title, request.Username);
            }
            catch (Exception exception)
            {
                throw new ServiceException<UserService>("Register failed", exception);
            }

            return response;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The service login response.</returns>
        public async Task<ServiceResponse<LoginResponse>> Login(LoginRequest request)
        {
            var response = new ServiceResponse<LoginResponse>();

            try
            {
                request.ValidateAndThrow(new LoginRequestValidator());
                response.Data.Token = await this.identityWrapper.AuthenticateAsync(request.Username, request.Password);
            }
            catch (Exception exception) when (IsClientError(exception))
            {
                AddErrors(exception, response.Status);
                this.logger.Warning("{Error} encountered by {Username} on login", response.Status.Error.Title, request.Username);
            }
            catch (Exception exception)
            {
                throw new ServiceException<UserService>("Login failed", exception);
            }

            return response;
        }
    }
}
