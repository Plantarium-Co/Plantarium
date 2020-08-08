// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Logging.Interfaces
{
    /// <summary>
    /// The logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Logs an information.
        /// </summary>
        /// <param name="message">The message.</param>
        void Information(string message);

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);
    }
}