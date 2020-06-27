// -----------------------------------------------------------------------
// <copyright file="IDbConnectionFactory.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Factories.Interfaces
{
    using System.Data.SqlClient;

    /// <summary>
    /// The db connection factory.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates the SQL connection.
        /// </summary>
        /// <returns>The SQL connection.</returns>
        SqlConnection CreateSqlConnection();
    }
}