// -----------------------------------------------------------------------
// <copyright file="ISqlHelper.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Helpers.Interfaces
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading.Tasks;

    /// <summary>
    /// The SQL helper.
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// Executes the command asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The task.</returns>
        Task ExecuteAsync(DbCommand command);

        /// <summary>
        /// Queries the result asynchronous.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>The list of query result.</returns>
        Task<IEnumerable<T>> QueryAsync<T>(DbCommand command) where T : class, new();

        /// <summary>
        /// Queries the first result asynchronous.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>The first query result.</returns>
        Task<T> QueryFirstAsync<T>(DbCommand command);
    }
}