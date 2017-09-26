using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.ModelBinding;
using System;

using Seeker.Models.Pages;
using Seeker.Models.Security;
using Seeker.Actors;
using Seeker.Messages;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a singn in module.
    /// </summary>
    public sealed class SignInModule : NancyModule
    {
        #region Private fields

        private readonly IUserManager _userManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a sign in module.
        /// </summary>
        /// <param name="userManager">A user manager.</param>
        public SignInModule(IUserManager userManager)
        {
            _userManager = userManager;

            Get("/signin", parameters =>
            {
                var model = this.Bind<ErrorSignInModel>();
                return Negotiate
                    .WithModel(model)
                    .WithView("signin");
            });

            Post("/signin", parameters =>
            {
                var login = this.BindAndValidate<LoginModel>();
                var userGuid = _userManager.Validate(login.UserName, login.UserPassword);

                if (userGuid == null)
                {
                    return this.Context.GetRedirect(string.Format("~/signin?error=true&username={0}", login.UserName));
                }

                SeekerContext.ActorSystem.ActorSelection(ActorPaths.UserManager.Path)
                    .Tell(new UserLoggedInMessage(login.UserName));

                DateTime? expiry = null;
                if (login.RememberMe)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            });
        }

        #endregion
    }
}
