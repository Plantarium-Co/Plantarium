// -----------------------------------------------------------------------
// <copyright file="FileLogger.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Logging
{
    using System;
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
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        public void Information(string messageTemplate, params object[] args)
        {
            this.logger.Information(messageTemplate, args);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        public void Warning(string messageTemplate, params object[] args)
        {
            this.logger.Warning(messageTemplate, args);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        public void Error(Exception exception, string messageTemplate, params object[] args)
        {
            this.logger.Error(exception, messageTemplate, args);
        }
    }
}
