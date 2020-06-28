// -----------------------------------------------------------------------
// <copyright file="UserServiceException.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Exceptions
{
    using System;

    /// <summary>
    /// The user service exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public UserServiceException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
