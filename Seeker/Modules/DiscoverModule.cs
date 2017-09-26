using Nancy;
using Nancy.ModelBinding;

using Seeker.Models;
using Seeker.Searching;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a discover page module.
    /// </summary>
    public class DiscoverModule : NancyModule
    {
        #region Private fields

        private readonly LuceneWrapper _lucene;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a discover page module.
        /// </summary>
        /// <param name="lucene">A Lucene client.</param>
        public DiscoverModule(LuceneWrapper lucene)
        {
            _lucene = lucene;

            var paginator = new Paginator();

            Get("/discover", parameters =>
            {
                var request = this.BindAndValidate<SearchRequest>();

                var searchResult = _lucene.Search(request);

                ViewBag.Query = request.Query;

                var pageBlock = paginator.GetPages(string.Format("{0}{1}", Request.Url.SiteBase, Request.Url.Path), request, searchResult, 2);

                var model = new PagedResult<LogEventData>(searchResult.Data, pageBlock.Pages, searchResult.TotalCount, pageBlock.TotalPages);

                return Negotiate
                    .WithModel(model)
                    .WithView("discover.html");
            });
        }

        #endregion
    }
}
