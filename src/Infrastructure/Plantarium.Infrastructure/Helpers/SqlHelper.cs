// -----------------------------------------------------------------------
// <copyright file="SqlHelper.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Plantarium.Infrastructure.Extensions;
    using Plantarium.Infrastructure.Factories.Interfaces;
    using Plantarium.Infrastructure.Helpers.Interfaces;

    /// <summary>
    /// The SQL helper.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Helpers.Interfaces.IDbHelper" />
    public class SqlHelper : IDbHelper
    {
        /// <summary>
        /// The database connection factory
        /// </summary>
        private readonly IDbConnectionFactory dbConnectionFactory;

        /// <summary>
        /// The transaction scope factory
        /// </summary>
        private readonly ITransactionScopeFactory transactionScopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlHelper"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="transactionScopeFactory">The transaction scope factory.</param>
        public SqlHelper(IDbConnectionFactory dbConnectionFactory, ITransactionScopeFactory transactionScopeFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.transactionScopeFactory = transactionScopeFactory;
        }

        /// <summary>
        /// Executes the command asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The task.</returns>
        public async Task ExecuteAsync(DbCommand command)
        {
            using (var transactionScope = this.transactionScopeFactory.CreateSerializableScope())
            using (var connection = this.dbConnectionFactory.CreateSqlConnection())
            {
                try
                {
                    command.Connection = connection;
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    transactionScope.Complete();
                }
                catch (Exception ex)
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

            using (var transactionScope = this.transactionScopeFactory.CreateReadUncommittedScope())
            using (var connection = this.dbConnectionFactory.CreateSqlConnection())
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
                catch (Exception ex)
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

            using (var transactionScope = this.transactionScopeFactory.CreateReadUncommittedScope())
            using (var connection = this.dbConnectionFactory.CreateSqlConnection())
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }
    }
}
