// -----------------------------------------------------------------------
// <copyright file="ResponseError.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The response error.
    /// </summary>
    public class ResponseError
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> Errors { get; } = new List<string>();
    }
}
