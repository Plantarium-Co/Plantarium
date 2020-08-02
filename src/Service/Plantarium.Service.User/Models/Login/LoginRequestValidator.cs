// -----------------------------------------------------------------------
// <copyright file="LoginRequestValidator.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Service.User.Models.Login
{
    using FluentValidation;

    /// <summary>
    /// The login request validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Plantarium.Service.User.Models.Login.LoginRequest}" />
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRequestValidator"/> class.
        /// </summary>
        public LoginRequestValidator()
        {
            this.RuleFor(request => request.Username).NotEmpty();
            this.RuleFor(request => request.Password).NotEmpty();
        }
    }
}
