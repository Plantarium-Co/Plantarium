// -----------------------------------------------------------------------
// <copyright file="SqlCommandBuilder.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Plantarium.Infrastructure.Builders.Interfaces;
    using static Plantarium.Infrastructure.Internals.ReflectionUtilities;

    /// <summary>
    /// The SQL command builder.
    /// </summary>
    /// <seealso cref="Plantarium.Infrastructure.Builders.DbCommandBuilder" />
    public class SqlCommandBuilder : DbCommandBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCommandBuilder"/> class.
        /// </summary>
        public SqlCommandBuilder() : base(new SqlCommand())
        {
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The db command builder.
        /// </returns>
        public override IDbCommandBuilder WithParameter(string name, object value)
        {
            var parameter = new SqlParameter(this.FormatParameterName(name), value);
            this.DbCommand.Parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// Adds parameters based on the model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The db command builder.
        /// </returns>
        public override IDbCommandBuilder WithParameters<T>(T model)
        {
            var properties = CachePropertyGetters<T>();
            var parameters = properties.Select(property => new SqlParameter(this.FormatParameterName(property.Key), property.Value(model) ?? DBNull.Value));
            this.DbCommand.Parameters.AddRange(parameters.ToArray());

            return this;
        }

        /// <summary>
        /// Adds parameters based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// The db command builder.
        /// </returns>
        public override IDbCommandBuilder WithParameters<T>(T model, Func<T, object> selector)
        {
            var selectedModel = selector(model);
            var properties = CachePropertyGetters(selectedModel);
            var parameters = properties.Select(property => new SqlParameter(this.FormatParameterName(property.Key), property.Value(selectedModel) ?? DBNull.Value));
            this.DbCommand.Parameters.AddRange(parameters.ToArray());

            return this;
        }

        /// <summary>
        /// Adds a data table parameter based on a model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="models">The models.</param>
        /// <returns>
        /// The db command builder.
        /// </returns>
        public override IDbCommandBuilder WithDataTableParameter<T>(string name, IEnumerable<T> models)
        {
            var properties = CachePropertyGetters<T>();
            var dataTable = this.CreateDataTable(models, properties);
            var parameter = new SqlParameter(this.FormatParameterName(name), dataTable) { SqlDbType = SqlDbType.Structured };
            this.DbCommand.Parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// Adds a data table parameter based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="models">The models.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// The db command builder.
        /// </returns>
        public override IDbCommandBuilder WithDataTableParameter<T>(string name, IEnumerable<T> models, Func<T, object> selector)
        {
            var selectedModels = models.Select(model => selector(model));
            var properties = CachePropertyGetters(selectedModels.First());
            var dataTable = this.CreateDataTable(selectedModels, properties);
            var parameter = new SqlParameter(this.FormatParameterName(name), dataTable) { SqlDbType = SqlDbType.Structured };
            this.DbCommand.Parameters.Add(parameter);

            return this;
        }
    }
}
