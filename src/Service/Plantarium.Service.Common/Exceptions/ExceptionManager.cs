// -----------------------------------------------------------------------
// <copyright file="ExceptionManager.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Exceptions
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Plantarium.Infrastructure.Exceptions;
    using Plantarium.Service.Common.Models;

    /// <summary>
    /// The exception manager.
    /// </summary>
    public static class ExceptionManager
    {
        /// <summary>
        /// Determines whether [is client error] [the specified exception].
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if [is client error] [the specified exception]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsClientError(Exception exception)
        {
            return exception is ValidationException || exception is IdentityException;
        }

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="responseStatus">The response status.</param>
        public static void AddErrors(Exception exception, ResponseStatus responseStatus)
        {
            switch (exception)
            {
                case ValidationException validationException:
                    responseStatus.Error.Title = "Validation Failed";
                    responseStatus.Error.Errors.AddRange(validationException.Errors.Select(error => error.ErrorMessage));
                    break;
                case IdentityException identityException:
                    responseStatus.Error.Title = identityException.Message;
                    responseStatus.Error.Errors.AddRange(identityException.Errors);
                    break;
                default:
                    throw exception;
            }
        }
    }
}
