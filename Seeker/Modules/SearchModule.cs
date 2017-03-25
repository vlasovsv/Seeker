using System;
using System.Linq;

using Autofac;
using Nancy;

using Seeker.Model;
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
        {
            _lucene = AutofacContext.Container.Resolve<LuceneWrapper>();

            Get("/search", parameters =>
            {
                var results = _lucene.Search((string)this.Request.Query["q"]);
                return Response.AsJson(results);
            });
        }

        #endregion
    }
}
