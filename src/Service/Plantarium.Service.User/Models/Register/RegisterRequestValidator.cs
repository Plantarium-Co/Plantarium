// -----------------------------------------------------------------------
// <copyright file="RegisterRequestValidator.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Models.Register
{
    using FluentValidation;

    /// <summary>
    /// The register request validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Plantarium.Service.User.Models.Register.RegisterRequest}" />
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRequestValidator"/> class.
        /// </summary>
        public RegisterRequestValidator()
        {
            this.RuleFor(request => request.Email).NotEmpty();
            this.RuleFor(request => request.Username).NotEmpty();
            this.RuleFor(request => request.Password).NotEmpty();
            this.RuleFor(request => request.Role).NotEmpty().IsInEnum();
            this.RuleFor(request => request.GivenName).NotEmpty();
            this.RuleFor(request => request.LastName).NotEmpty();
        }
    }
}
