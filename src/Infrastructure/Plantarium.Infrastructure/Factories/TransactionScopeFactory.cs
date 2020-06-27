// -----------------------------------------------------------------------
// <copyright file="TransactionScopeFactory.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Factories
{
    using System.Transactions;
    using Plantarium.Infrastructure.Factories.Interfaces;

    /// <summary>
    /// The transaction scope factory.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Factories.Interfaces.ITransactionScopeFactory" />
    public class TransactionScopeFactory : ITransactionScopeFactory
    {
        /// <summary>
        /// The serializable option
        /// </summary>
        private static readonly TransactionOptions SerializableOption = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };

        /// <summary>
        /// The read uncommitted option
        /// </summary>
        private static readonly TransactionOptions ReadUncommittedOption = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionScopeFactory"/> class.
        /// </summary>
        public TransactionScopeFactory()
        {
        }

        /// <summary>
        /// Creates a serializable scope.
        /// </summary>
        /// <returns>
        /// The transaction scope.
        /// </returns>
        public TransactionScope CreateSerializableScope()
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew, SerializableOption, TransactionScopeAsyncFlowOption.Enabled);
        }

        /// <summary>
        /// Creates a read uncommitted scope.
        /// </summary>
        /// <returns>
        /// The transaction scope.
        /// </returns>
        public TransactionScope CreateReadUncommittedScope()
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew, ReadUncommittedOption, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
