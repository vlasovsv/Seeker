using Nancy;
using Nancy.Authentication.Forms;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a singn out module.
    /// </summary>
    public sealed class SignOutModule : NancyModule
    {
        #region Constructors

        /// <summary>
        /// Creates a sign out module.
        /// </summary>
        public SignOutModule()
        {
            Get("/signout", parameters =>
            {
                return this.LogoutAndRedirect("~/signin");
            });
        }

        #endregion
    }
}
