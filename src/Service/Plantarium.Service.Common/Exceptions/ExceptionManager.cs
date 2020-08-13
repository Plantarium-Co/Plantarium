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
        /// Adds the errors.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="responseStatus">The response status.</param>
        /// <exception cref="ServiceException">The service exception.</exception>
        public static void AddErrors(Exception ex, ResponseStatus responseStatus)
        {
            switch (ex)
            {
                case ValidationException validationException:
                    responseStatus.Error.Title = "Validation Failed";
                    responseStatus.Error.Errors.AddRange(validationException.Errors.Select(error => error.ErrorMessage));
                    break;
                case IdentityException identityException:
                    responseStatus.Error.Title = identityException.Message;
                    responseStatus.Error.Errors.AddRange(identityException.Errors);
                    break;
                case ServiceDataException serviceDataException:
                    throw serviceDataException;
                default:
                    throw new ServiceException(ex.Message, ex);
            }
        }
    }
}
