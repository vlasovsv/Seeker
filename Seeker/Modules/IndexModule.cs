using System;

using Autofac;
using Nancy;

using Seeker.Model;
using Seeker.Searching;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents an index page module.
    /// </summary>
    public class IndexModule : NancyModule
    {
        #region Private fields

        private readonly LuceneWrapper _lucene;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an index page module.
        /// </summary>
        public IndexModule()
        {
            _lucene = AutofacContext.Container.Resolve<LuceneWrapper>();
            var paginator = new Paginator();

            Get("/", parameters =>
            {
                var request = new SearchRequest();
                var to = DateTime.Now.AddDays(1);
                var from = to.AddDays(-1);
                request.Query = string.Format("Timestamp:[{0} TO {1}]", from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"));
                request.Offset = 100;

                var searchResult = _lucene.Search(request);
                var index = new IndexModel();
                index.Results = searchResult.Data;

                ViewBag.Query = string.Empty;

                return Negotiate.WithModel(index);
            });
        }

        #endregion
    }
}
