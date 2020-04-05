// -----------------------------------------------------------------------
// <copyright file="IdentityException.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The authentication exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class IdentityException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public IdentityException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public IdentityException(string message, IEnumerable<IdentityError> errors)
            : base(message)
        {
            this.Errors = errors.ToDictionary(a => a.Code, a => a.Description);
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IDictionary<string, string> Errors { get; private set; }
    }
}
