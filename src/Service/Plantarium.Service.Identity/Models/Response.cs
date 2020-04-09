// -----------------------------------------------------------------------
// <copyright file="Response.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Identity.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The base service response.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="Response"/> is succeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeded; otherwise, <c>false</c>.
        /// </value>
        public bool Succeded
        {
            get => this.Errors.Any() ? false : true;
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> Errors { get; set; } = new List<string>();
    }
}
