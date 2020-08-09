// -----------------------------------------------------------------------
// <copyright file="IdentityWrapper.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Wrappers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Plantarium.Domain.Constants;
    using Plantarium.Infrastructure.Exceptions;
    using Plantarium.Infrastructure.Providers.Interfaces;
    using Plantarium.Infrastructure.Wrappers.Interfaces;

    /// <summary>
    /// The authentication wrapper.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Wrappers.Interfaces.IIdentityWrapper" />
    public class IdentityWrapper : IIdentityWrapper
    {
        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<IdentityUser<Guid>> userManager;

        /// <summary>
        /// The sign in manager.
        /// </summary>
        private readonly SignInManager<IdentityUser<Guid>> signInManager;

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        /// <summary>
        /// The token provider.
        /// </summary>
        private readonly ITokenProvider tokenProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityWrapper"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="tokenProvider">The token provider.</param>
        /// <exception cref="ArgumentNullException">
        /// userManager
        /// or
        /// signInManager
        /// or
        /// roleManager
        /// or
        /// tokenProvider
        /// </exception>
        public IdentityWrapper(
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITokenProvider tokenProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The identity user id.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.IdentityException">
        /// Registration failed.
        /// or
        /// Claim Registration failed.
        /// </exception>
        public async Task<Guid> RegisterAsync(string email, string username, string password)
        {
            var user = new IdentityUser<Guid>(username) { Email = email };
            var createResult = await this.userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                throw new IdentityException("Registration failed", createResult.Errors);
            }

            var registerClaimResult = await this.RegisterClaimsAsync(username);

            if (!registerClaimResult.Succeeded)
            {
                throw new IdentityException("Claim registration failed", registerClaimResult.Errors);
            }

            user = await this.userManager.FindByNameAsync(username);

            return user.Id;
        }

        /// <summary>
        /// Adds to role asynchronous.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.IdentityException">
        /// Role creation failed.
        /// or
        /// Role assignment failed.
        /// or
        /// Role claim assignment failed.
        /// </exception>
        public async Task AddToRoleAsync(string username, Role role)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var addRoleResult = await this.userManager.AddToRoleAsync(user, role.ToString());

            if (!addRoleResult.Succeeded)
            {
                throw new IdentityException("Role assignment failed", addRoleResult.Errors);
            }

            var addRoleClaimResult = await this.userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role.ToString()));

            if (!addRoleClaimResult.Succeeded)
            {
                throw new IdentityException("Role claim assignment failed", addRoleClaimResult.Errors);
            }
        }

        /// <summary>
        /// Removes from role asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.IdentityException">
        /// Role removal failed.
        /// or
        /// Role claim removal failed.
        /// </exception>
        public async Task RemoveFromRoleAsync(string username, Role role)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var removeRoleResult = await this.userManager.RemoveFromRoleAsync(user, role.ToString());

            if (!removeRoleResult.Succeeded)
            {
                throw new IdentityException("Role removal failed", removeRoleResult.Errors);
            }

            var removeRoleClaimResult = await this.userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, role.ToString()));

            if (!removeRoleClaimResult.Succeeded)
            {
                throw new IdentityException("Role claim removal failed", removeRoleClaimResult.Errors);
            }
        }

        /// <summary>
        /// Authenticates the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token generated.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.IdentityException">Sign in failed.</exception>
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var identifiedUsername = await this.IdentifyUsernameAsync(username);
            var signInResult = await this.signInManager.PasswordSignInAsync(identifiedUsername, password, false, true);

            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    throw new IdentityException("Sign in failed", new[] { new IdentityError { Description = "Too many failed attempts. Try again later." } });
                }

                throw new IdentityException("Sign in failed", new[] { new IdentityError { Description = "Username or Password is incorrect." } });
            }

            var user = await this.userManager.FindByNameAsync(identifiedUsername);
            var claims = await this.userManager.GetClaimsAsync(user);
            var result = this.tokenProvider.GenerateToken(claims);

            return result;
        }

        /// <summary>
        /// Registers the claims asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The identity result.</returns>
        private async Task<IdentityResult> RegisterClaimsAsync(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var result = await this.userManager.AddClaimsAsync(user, claims);

            return result;
        }

        /// <summary>
        /// Identifies the username asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The identified username.</returns>
        private async Task<string> IdentifyUsernameAsync(string username)
        {
            var result = username;

            if (username.Contains("@"))
            {
                var user = await this.userManager.FindByEmailAsync(username);

                if (user != null)
                {
                    result = user.UserName;
                }
            }

            return result;
        }
    }
}
