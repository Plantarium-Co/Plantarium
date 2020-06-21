// -----------------------------------------------------------------------
// <copyright file="SqlHelper.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Transactions;
    using Plantarium.Infrastructure.Extensions;
    using Plantarium.Infrastructure.Helpers.Interfaces;

    /// <summary>
    /// The SQL helper.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Helpers.Interfaces.ISqlHelper" />
    public class SqlHelper : ISqlHelper
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlHelper"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Executes the command asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The task.</returns>
        public async Task ExecuteAsync(DbCommand command)
        {
            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
            using (var connection = new SqlConnection(this.connectionString))
            {
                try
                {
                    command.Connection = connection;
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    transactionScope.Complete();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Queries the result asynchronous.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The list of query result.
        /// </returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(DbCommand command) where T : class, new()
        {
            var result = default(IEnumerable<T>);
            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
            using (var connection = new SqlConnection(this.connectionString))
            {
                try
                {
                    command.Connection = connection;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            result = reader.Map<T>();
                        }
                    }

                    transactionScope.Complete();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// Queries the first result asynchronous.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The first query result.
        /// </returns>
        public async Task<T> QueryFirstAsync<T>(DbCommand command)
        {
            var result = default(T);
            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
            using (var connection = new SqlConnection(this.connectionString))
            {
                try
                {
                    command.Connection = connection;
                    await connection.OpenAsync();
                    var firstRow = await command.ExecuteScalarAsync();

                    if (firstRow is T)
                    {
                        result = (T)firstRow;
                    }

                    transactionScope.Complete();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }

            return result;
        }
    }
}
