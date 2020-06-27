// -----------------------------------------------------------------------
// <copyright file="IDbCommandBuilderFactory.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Factories.Interfaces
{
    using Plantarium.Infrastructure.Builders.Interfaces;

    /// <summary>
    /// The db command builder factory.
    /// </summary>
    public interface IDbCommandBuilderFactory
    {
        /// <summary>
        /// Creates the SQL command builder.
        /// </summary>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder CreateSqlCommandBuilder();
    }
}