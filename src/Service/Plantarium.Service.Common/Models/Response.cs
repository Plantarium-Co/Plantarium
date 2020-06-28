// -----------------------------------------------------------------------
// <copyright file="Response.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------

namespace Plantarium.Service.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The base response.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="Response"/> is succeed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success => this.Errors.Any();

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> Errors { get; set; } = new List<string>();
    }
}
