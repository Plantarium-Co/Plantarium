﻿// -----------------------------------------------------------------------
// <copyright file="AuthWrapper.cs" company="Plantarium Co.">
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
    using Plantarium.Infrastructure.Exceptions;
    using Plantarium.Infrastructure.Providers.Interfaces;
    using Plantarium.Infrastructure.Wrappers.Interfaces;

    /// <summary>
    /// The authentication wrapper.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Wrappers.Interfaces.IAuthWrapper" />
    public class AuthWrapper : IAuthWrapper
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
        /// Initializes a new instance of the <see cref="AuthWrapper"/> class.
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
        public AuthWrapper(
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITokenProvider tokenProvider)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The task.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.AuthException">
        /// Registration failed.
        /// or
        /// Claim Registration failed.
        /// </exception>
        public async Task RegisterAsync(string username, string password)
        {
            var createResult = await this.userManager.CreateAsync(new IdentityUser<Guid>(username), password);

            if (!createResult.Succeeded)
            {
                throw new AuthException("Registration failed.", createResult.Errors);
            }

            var registerClaimResult = await this.RegisterClaimsAsync(username);

            if (!registerClaimResult.Succeeded)
            {
                throw new AuthException("Claim Registration failed.", registerClaimResult.Errors);
            }
        }

        /// <summary>
        /// Adds to role asynchronous.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.AuthException">
        /// Role creation failed.
        /// or
        /// Role assignment failed.
        /// or
        /// Role claim assignment failed.
        /// </exception>
        public async Task AddToRoleAsync(string username, string role)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var addRoleResult = await this.userManager.AddToRoleAsync(user, role);

            if (!addRoleResult.Succeeded)
            {
                throw new AuthException("Role assignment failed.", addRoleResult.Errors);
            }

            var addRoleClaimResult = await this.userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));

            if (!addRoleClaimResult.Succeeded)
            {
                throw new AuthException("Role claim assignment failed.", addRoleClaimResult.Errors);
            }
        }

        /// <summary>
        /// Removes from role asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.AuthException">
        /// Role removal failed.
        /// or
        /// Role claim removal failed.
        /// </exception>
        public async Task RemoveFromRoleAsync(string username, string role)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var removeRoleResult = await this.userManager.RemoveFromRoleAsync(user, role);

            if (!removeRoleResult.Succeeded)
            {
                throw new AuthException("Role removal failed.", removeRoleResult.Errors);
            }

            var removeRoleClaimResult = await this.userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, role));

            if (!removeRoleClaimResult.Succeeded)
            {
                throw new AuthException("Role claim removal failed.", removeRoleClaimResult.Errors);
            }
        }

        /// <summary>
        /// Authenticates the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token generated.</returns>
        /// <exception cref="Plantarium.Infrastructure.Exceptions.AuthException">Sign in failed.</exception>
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var signInResult = await this.signInManager.PasswordSignInAsync(username, password, false, false);

            if (!signInResult.Succeeded)
            {
                throw new AuthException("Sign in failed.");
            }

            var user = await this.userManager.FindByNameAsync(username);
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
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var result = await this.userManager.AddClaimsAsync(user, claims);

            return result;
        }
    }
}