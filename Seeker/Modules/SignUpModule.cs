using System.Linq;

using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;

using Seeker.Models.Pages;
using Seeker.Models.Security;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a sign up module.
    /// </summary>
    public sealed class SignUpModule : NancyModule
    {
        #region Private fields

        private readonly IUserManager _userManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a sign up module.
        /// </summary>
        /// <param name="userManager">A user manager.</param>
        public SignUpModule(IUserManager userManager)
        {
            _userManager = userManager;

            Get("/signup", parameters =>
            {
                var model = this.Bind<ErrorSignUpModel>();
                return Negotiate
                    .WithModel(model)
                    .WithView("signup");
            });

            Post("/signup", parameters =>
            {
                var registration = this.BindAndValidate<UserRegistrationModel>();

                if (Context.ModelValidationResult.IsValid)
                {
                    _userManager.Register(registration.UserName, registration.UserPassword);
                    return Response.AsRedirect("/");
                }
                else
                {
                    return this.Context.GetRedirect(string.Format("~/signup?error=true"));
                }
                
            });
        }

        #endregion
    }
}
