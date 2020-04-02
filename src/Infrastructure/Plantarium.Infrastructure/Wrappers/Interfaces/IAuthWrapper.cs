// -----------------------------------------------------------------------
// <copyright file="IAuthWrapper.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Wrappers.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// The authentication wrapper.
    /// </summary>
    public interface IAuthWrapper
    {
        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The task.</returns>
        Task RegisterAsync(string username, string password);

        /// <summary>
        /// Adds to role asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        Task AddToRoleAsync(string username, string role);

        /// <summary>
        /// Removes from role asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>The task.</returns>
        Task RemoveFromRoleAsync(string username, string role);

        /// <summary>
        /// Authenticates the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token generated.</returns>
        Task<string> AuthenticateAsync(string username, string password);
    }
}
