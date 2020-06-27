// -----------------------------------------------------------------------
// <copyright file="ITransactionScopeFactory.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Factories.Interfaces
{
    using System.Transactions;

    /// <summary>
    /// The transaction scope factory.
    /// </summary>
    public interface ITransactionScopeFactory
    {
        /// <summary>
        /// Creates a read uncommitted scope.
        /// </summary>
        /// <returns>The transaction scope.</returns>
        TransactionScope CreateReadUncommittedScope();

        /// <summary>
        /// Creates a serializable scope.
        /// </summary>
        /// <returns>The transaction scope.</returns>
        TransactionScope CreateSerializableScope();
    }
}