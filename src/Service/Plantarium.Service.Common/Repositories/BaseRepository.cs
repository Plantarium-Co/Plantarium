// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Repositories
{
    using Plantarium.Infrastructure.Factories.Interfaces;
    using Plantarium.Infrastructure.Helpers.Interfaces;

    /// <summary>
    /// The base repository.
    /// </summary>
    public abstract class BaseRepository
    {
        /// <summary>
        /// The default timeout.
        /// </summary>
        protected static readonly int DefaultTimeout = 10;

        /// <summary>
        /// The builder factory.
        /// </summary>
        protected readonly IDbCommandBuilderFactory BuilderFactory;

        /// <summary>
        /// The database helper.
        /// </summary>
        protected readonly IDbHelper DbHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="builderFactory">The builder factory.</param>
        /// <param name="dbHelper">The database helper.</param>
        public BaseRepository(IDbCommandBuilderFactory builderFactory, IDbHelper dbHelper)
        {
            this.BuilderFactory = builderFactory;
            this.DbHelper = dbHelper;
        }
    }
}
