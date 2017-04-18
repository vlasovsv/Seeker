using System.Linq;

using Autofac;
using Nancy;

using Seeker.Searching;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a controller for searching.
    /// </summary>
    public sealed class SearchModule : NancyModule
    {
        #region Private fields

        private readonly LuceneWrapper _lucene;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a search module.
        /// </summary>
        public SearchModule()
            : base("api/v1/")
        {
            _lucene = AutofacContext.Container.Resolve<LuceneWrapper>();

            Get("/search", parameters =>
            {
                var request = new SearchRequest();
                request.Query = this.Request.Query["q"];
                var results = _lucene.Search(request);
                return Response.AsJson(results.OrderByDescending(x => x.Timestamp));
            });
        }

        #endregion
    }
}
