// -----------------------------------------------------------------------
// <copyright file="Request.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.Common.Models
{
    using FluentValidation;

    /// <summary>
    /// The base request.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Validates the and throw.
        /// </summary>
        /// <typeparam name="T">The request type.</typeparam>
        /// <param name="validator">The validator.</param>
        public void ValidateAndThrow<T>(IValidator<T> validator) where T : Request
        {
            validator.ValidateAndThrow((T)this);
        }
    }
}
