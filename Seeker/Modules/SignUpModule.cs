using Nancy;
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

        private readonly IUserManager userManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a sign up module.
        /// </summary>
        /// <param name="userManager">A user manager.</param>
        public SignUpModule(IUserManager userManager)
        {
            Get("/signup", parameters =>
            {
                return Negotiate.WithView("signup");
            });

            Post("/signup", parameters =>
            {
                var registration = this.BindAndValidate<UserRegistrationModel>();

                userManager.Register(registration.UserName, registration.UserPassword);

                return Response.AsRedirect("/");
            });
            this.userManager = userManager;
        }

        #endregion
    }
}
