// -----------------------------------------------------------------------
// <copyright file="ServiceDataException.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Exceptions
{
    using System;

    /// <summary>
    /// The service data exception.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <seealso cref="Plantarium.Service.Common.Exceptions.ServiceDataException" />
    /// <seealso cref="System.Exception" />
    public class ServiceDataException<T> : ServiceDataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataException{T}"/> class.
        /// </summary>
        public ServiceDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataException{T}"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceDataException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataException{T}"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ServiceDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// The service data exception.
    /// </summary>
    /// <seealso cref="Plantarium.Service.Common.Exceptions.ServiceDataException" />
    public class ServiceDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataException" /> class.
        /// </summary>
        public ServiceDataException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceDataException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDataException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ServiceDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
