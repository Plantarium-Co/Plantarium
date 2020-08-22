// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Logging.Interfaces
{
    using System;

    /// <summary>
    /// The logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        void Error(Exception exception, string messageTemplate, params object[] args);

        /// <summary>
        /// Logs an information.
        /// </summary>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        void Information(string messageTemplate, params object[] args);

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        void Warning(string messageTemplate, params object[] args);
    }
}