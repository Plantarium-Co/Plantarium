// -----------------------------------------------------------------------
// <copyright file="ResponseStatus.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Models
{
    using System.Linq;

    /// <summary>
    /// The response status.
    /// </summary>
    public class ResponseStatus
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ResponseStatus"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success => !this.Error.Errors.Any();

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public ResponseError Error { get; } = new ResponseError();
    }
}
