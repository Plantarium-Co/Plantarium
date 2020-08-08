// -----------------------------------------------------------------------
// <copyright file="FileLogger.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Logging
{
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Internal = Interfaces;

    /// <summary>
    /// The file logger.
    /// </summary>
    public class FileLogger : Internal.ILogger
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public FileLogger(IConfiguration configuration)
        {
            this.logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration, "FileLogging")
                .CreateLogger();
        }

        /// <summary>
        /// Logs an information.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Information(string message)
        {
            this.logger.Information(message);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(string message)
        {
            this.logger.Warning(message);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            this.logger.Error(message);
        }
    }
}
