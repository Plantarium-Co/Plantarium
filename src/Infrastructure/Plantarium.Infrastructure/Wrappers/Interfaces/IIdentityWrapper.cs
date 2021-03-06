﻿// -----------------------------------------------------------------------
// <copyright file="IIdentityWrapper.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Wrappers.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Plantarium.Domain.Constants;

    /// <summary>
    /// The identity wrapper.
    /// </summary>
    public interface IIdentityWrapper
    {
        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The identity user id.</returns>
        Task<Guid> RegisterAsync(string email, string username, string password);

        /// <summary>
        /// Adds to role asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        Task AddToRoleAsync(string username, Role role);

        /// <summary>
        /// Removes from role asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        Task RemoveFromRoleAsync(string username, Role role);

        /// <summary>
        /// Authenticates the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token generated.</returns>
        Task<string> AuthenticateAsync(string username, string password);
    }
}
