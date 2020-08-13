// -----------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Api.Middlewares
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Plantarium.Infrastructure.Logging.Interfaces;

    /// <summary>
    /// Exception handling middleware.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        /// <summary>
        /// The next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The hosting environment.
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="logger">The logger.</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IHostingEnvironment hostingEnvironment,
            ILogger logger)
        {
            this.next = next;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>The task.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            this.logger.Error(ex.Message, ex);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (this.hostingEnvironment.IsProduction())
            {
                return context.Response.WriteAsync("Service unavailable. Try again later.");
            }

            return context.Response.WriteAsync(ex.Message);
        }
    }
}
