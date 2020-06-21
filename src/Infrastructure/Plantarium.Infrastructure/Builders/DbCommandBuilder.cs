// -----------------------------------------------------------------------
// <copyright file="DbCommandBuilder.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    /// <summary>
    /// The db command builder.
    /// </summary>
    public abstract class DbCommandBuilder
    {
        /// <summary>
        /// The database command.
        /// </summary>
        protected readonly DbCommand DbCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandBuilder"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        public DbCommandBuilder(DbCommand dbCommand)
        {
            this.DbCommand = dbCommand;
        }

        /// <summary>
        /// Specifies that the command is a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder IsStoredProcedure(string storedProcedureName);

        /// <summary>
        /// Adds parameters based on a model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithParameters<T>(T model);

        /// <summary>
        /// Adds parameters based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithParameters<T>(T model, Func<T, object> selector);

        /// <summary>
        /// Adds a data table parameter based on a model type..
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="models">The models.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithDataTableParameter<T>(IEnumerable<T> models);

        /// <summary>
        /// Adds a data table parameter based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="models">The models.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithDataTableParameter<T>(IEnumerable<T> models, Func<T, object> selector);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The db command.</returns>
        public DbCommand Build()
        {
            return this.DbCommand;
        }

        /// <summary>
        /// Creates the data table based on a model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="models">The models.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>The db command builder.</returns>
        protected DataTable CreateDataTable<T>(IEnumerable<T> models, Dictionary<string, Func<T, object>> properties)
        {
            var dataTable = new DataTable();

            var columns = properties.Select(property => new DataColumn(property.Key));
            dataTable.Columns.AddRange(columns.ToArray());

            foreach (var model in models)
            {
                var values = properties.Select(property => property.Value(model) ?? DBNull.Value);
                var row = dataTable.NewRow();
                row.ItemArray = values.ToArray();
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// Formats the name of the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The formatted parameter name.</returns>
        protected string FormatParameterName(string name)
        {
            return $"@{char.ToLowerInvariant(name[0])}{name.Substring(1)}";
        }
    }
}
