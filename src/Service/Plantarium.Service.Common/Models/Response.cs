// -----------------------------------------------------------------------
// <copyright file="Response.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Models
{
    /// <summary>
    /// The base response.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ResponseStatus Status { get; set; }
    }

    /// <summary>
    /// The base response with data.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public class Response<T> : Response
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
    }
}
