using FluentValidation;

using Seeker.Models.Pages;

namespace Seeker.Validation
{
    /// <summary>
    /// Represents a validator for user registration request.
    /// </summary>
    public sealed class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
    {
        #region Constructors

        /// <summary>
        /// Creates a new validator instance.
        /// </summary>
        public UserRegistrationModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();

            RuleFor(x => x.UserPassword)
                .Equal(x => x.ConfirmedUserPassword)
                .WithMessage("Passwords don't match");
        }

        #endregion
    }
}
