// -----------------------------------------------------------------------
// <copyright file="DbConnectionFactory.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Factories
{
    using System.Data.SqlClient;
    using Plantarium.Infrastructure.Factories.Interfaces;

    /// <summary>
    /// The db connection factory.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Factories.Interfaces.IDbConnectionFactory" />
    public class DbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Creates the SQL connection.
        /// </summary>
        /// <returns>
        /// The SQL connection.
        /// </returns>
        public SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(this.connectionString);
        }
    }
}
