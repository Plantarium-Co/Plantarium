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
    using System.Text.RegularExpressions;

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
        /// The parameter name format.
        /// </summary>
        private static readonly Regex ParameterNameFormat = new Regex("^[@][a-z].*");

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandBuilder"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        public DbCommandBuilder(DbCommand dbCommand)
        {
            this.DbCommand = dbCommand;
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithParameter(string name, object value);

        /// <summary>
        /// Adds parameters based on a model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithParameters<T>(T model) where T : class;

        /// <summary>
        /// Adds parameters based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithParameters<T>(T model, Func<T, object> selector) where T : class;

        /// <summary>
        /// Adds a data table parameter based on a model type..
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="models">The models.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithDataTableParameter<T>(string name, IEnumerable<T> models) where T : class;

        /// <summary>
        /// Adds a data table parameter based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="models">The models.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The db command builder.</returns>
        public abstract DbCommandBuilder WithDataTableParameter<T>(string name, IEnumerable<T> models, Func<T, object> selector) where T : class;

        /// <summary>
        /// Specifies that the command is a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>The db command builder.</returns>
        public virtual DbCommandBuilder IsStoredProcedure(string storedProcedureName)
        {
            this.DbCommand.CommandText = storedProcedureName;
            this.DbCommand.CommandType = CommandType.StoredProcedure;

            return this;
        }

        /// <summary>
        /// Sets the timeout.
        /// </summary>
        /// <param name="timeoutSeconds">The timeout seconds.</param>
        /// <returns>The db command builder.</returns>
        public virtual DbCommandBuilder WithTimeout(int timeoutSeconds)
        {
            this.DbCommand.CommandTimeout = timeoutSeconds;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The db command.</returns>
        public virtual DbCommand Build()
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
        protected virtual DataTable CreateDataTable<T>(IEnumerable<T> models, Dictionary<string, Func<T, object>> properties)
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
        protected virtual string FormatParameterName(string name)
        {
            var parameterName = name;

            if (!ParameterNameFormat.IsMatch(name))
            {
                parameterName = $"@{char.ToLowerInvariant(name[0])}{name.Substring(1)}";
            }

            return parameterName;
        }
    }
}
