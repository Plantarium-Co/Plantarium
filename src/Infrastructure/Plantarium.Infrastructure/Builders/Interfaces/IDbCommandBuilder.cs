// -----------------------------------------------------------------------
// <copyright file="IDbCommandBuilder.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Builders.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// The db command builder.
    /// </summary>
    public interface IDbCommandBuilder
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The db command.</returns>
        DbCommand Build();

        /// <summary>
        /// Specifies that the command is a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder IsStoredProcedure(string storedProcedureName);

        /// <summary>
        /// Adds a data table parameter based on a model type..
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="models">The models.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder WithDataTableParameter<T>(string name, IEnumerable<T> models) where T : class;

        /// <summary>
        /// Adds a data table parameter based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="models">The models.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder WithDataTableParameter<T>(string name, IEnumerable<T> models, Func<T, object> selector) where T : class;

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder WithParameter(string name, object value);

        /// <summary>
        /// Adds parameters based on a model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder WithParameters<T>(T model) where T : class;

        /// <summary>
        /// Adds parameters based on a selected model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder WithParameters<T>(T model, Func<T, object> selector) where T : class;

        /// <summary>
        /// Sets the timeout.
        /// </summary>
        /// <param name="timeoutSeconds">The timeout seconds.</param>
        /// <returns>The db command builder.</returns>
        IDbCommandBuilder WithTimeout(int timeoutSeconds);
    }
}