using Nancy;
using Nancy.Security;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents an index page module.
    /// </summary>
    public class IndexModule : NancyModule
    {
        #region Constructors

        /// <summary>
        /// Creates an index page module.
        /// </summary>
        public IndexModule()
        {
            this.RequiresAuthentication();

            Get("/", parameters =>
            {
                return Response.AsRedirect("/discover");
            });
        }

        #endregion
    }
}
