// -----------------------------------------------------------------------
// <copyright file="ResponseStatus.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The response status.
    /// </summary>
    public class ResponseStatus
    {
        /// <summary>
        /// The errors.
        /// </summary>
        private readonly List<string> errors = new List<string>();

        /// <summary>
        /// Gets a value indicating whether this <see cref="ResponseStatus"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success => this.errors.Any();

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public string[] Errors => this.errors.ToArray();

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        public void AddError(string error) => this.errors.Add(error);

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void AddErrors(IEnumerable<string> errors) => this.errors.AddRange(errors);
    }
}
