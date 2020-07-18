// -----------------------------------------------------------------------
// <copyright file="ServiceResponse.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Models
{
    /// <summary>
    /// The base service response.
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ResponseStatus Status { get; set; } = new ResponseStatus();
    }

    /// <summary>
    /// The base service response with data.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public class ServiceResponse<T> : ServiceResponse where T : Response, new()
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; } = new T();
    }
}
