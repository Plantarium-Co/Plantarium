// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Repositories.Interfaces
{
    using System.Threading.Tasks;
    using Plantarium.Domain.Entities.User;

    /// <summary>
    /// The user repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task.</returns>
        Task CreateUserAsync(User user);
    }
}