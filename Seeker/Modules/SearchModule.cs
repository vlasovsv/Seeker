using Autofac;
using System;

using Nancy;
using Nancy.ModelBinding;

using Seeker.Model;
using Seeker.Searching;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a search page module.
    /// </summary>
    public class SearchModule : NancyModule
    {
        #region Private fields

        private readonly LuceneWrapper _lucene;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a search page module.
        /// </summary>
        public SearchModule()
        {
            _lucene = AutofacContext.Container.Resolve<LuceneWrapper>();
            var paginator = new Paginator();

            Get("/search", parameters =>
            {
                var request = this.BindAndValidate<SearchRequest>();

                var searchResult = _lucene.Search(request);
                var model = new SearchModel();

                ViewBag.Query = request.Query;
                ViewBag.TotalCount = searchResult.TotalCount;


                model.Results = searchResult.Data;
                model.Pages = paginator.GetPages(string.Format("{0}{1}", Request.Url.SiteBase, Request.Url.Path), request, searchResult, 2);

                return Negotiate
                    .WithModel(model)
                    .WithView("search.html");
            });
        }

        #endregion
    }
}
