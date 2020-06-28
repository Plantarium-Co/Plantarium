// -----------------------------------------------------------------------
// <copyright file="UserServiceDataException.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Exceptions
{
    using System;

    /// <summary>
    /// The user service data exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserServiceDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceDataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UserServiceDataException(string message) : base(message)
        {
        }
    }
}
