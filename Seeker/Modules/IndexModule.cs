using Nancy;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a controller of a start searching page.
    /// </summary>
    public class IndexModule : NancyModule
    {
        #region Constructors

        /// <summary>
        /// Creates an index module.
        /// </summary>
        public IndexModule()
        {
            Get("/", _ =>
            {
                return Negotiate.WithView("index.html");
            });
        }

        #endregion
    }
}
