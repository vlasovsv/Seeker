using System.Linq;

using Autofac;
using Nancy;

using Seeker.Searching;
using System;

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
                var start = DateTime.Parse(this.Request.Query["start"]);
                var end = DateTime.Parse(this.Request.Query["end"]);
                var request = new SearchRequest(this.Request.Query["q"], start, end);
                var results = _lucene.Search(request);
                return Response.AsJson(results.OrderByDescending(x => x.Timestamp));
            });
        }

        #endregion
    }
}
