// -----------------------------------------------------------------------
// <copyright file="DbCommandBuilderFactory.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Factories
{
    using Plantarium.Infrastructure.Builders;
    using Plantarium.Infrastructure.Builders.Interfaces;
    using Plantarium.Infrastructure.Factories.Interfaces;

    /// <summary>
    /// The db command factory.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Factories.Interfaces.IDbCommandBuilderFactory" />
    public class DbCommandBuilderFactory : IDbCommandBuilderFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandBuilderFactory"/> class.
        /// </summary>
        public DbCommandBuilderFactory()
        {
        }

        /// <summary>
        /// Creates the SQL command builder.
        /// </summary>
        /// <returns>
        /// The db command builder.
        /// </returns>
        public IDbCommandBuilder CreateSqlCommandBuilder()
        {
            return new SqlCommandBuilder();
        }
    }
}
