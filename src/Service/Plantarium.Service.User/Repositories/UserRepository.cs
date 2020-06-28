// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Plantarium.Domain.Entities.User;
    using Plantarium.Infrastructure.Factories.Interfaces;
    using Plantarium.Infrastructure.Helpers.Interfaces;
    using Plantarium.Service.User.Exceptions;
    using Plantarium.Service.User.Repositories.Interfaces;

    /// <summary>
    /// The user repository.
    /// </summary>
    /// <seealso cref="Plantarium.Service.User.Repositories.Interfaces.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The default timeout.
        /// </summary>
        private static readonly int DefaultTimeout = 10;

        /// <summary>
        /// The builder factory.
        /// </summary>
        private readonly IDbCommandBuilderFactory builderFactory;

        /// <summary>
        /// The database helper.
        /// </summary>
        private readonly IDbHelper dbHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="builderFactory">The builder factory.</param>
        /// <param name="dbHelper">The database helper.</param>
        /// <exception cref="ArgumentNullException">
        /// builderFactory
        /// or
        /// dbHelper
        /// </exception>
        public UserRepository(IDbCommandBuilderFactory builderFactory, IDbHelper dbHelper)
        {
            this.builderFactory = builderFactory;
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task.</returns>
        public async Task CreateUserAsync(User user)
        {
            try
            {
                var command = this.builderFactory
                    .CreateSqlCommandBuilder()
                    .IsStoredProcedure("sp_CreateUser")
                    .WithTimeout(DefaultTimeout)
                    .WithParameters(user, data => new { data.IdentityId, data.GivenName, data.LastName })
                    .Build();

                await this.dbHelper.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                throw new UserServiceDataException(ex.Message);
            }
        }
    }
}
