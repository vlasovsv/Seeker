using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace Seeker.Models
{
    /// <summary>
    /// Represents a paginated result.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    public class PagedResult<T>
    {
        #region Constructors

        /// <summary>
        /// Creates a new paginated result.
        /// </summary>
        /// <param name="results">The collection of current results.</param>
        /// <param name="pages">The collection of pages.</param>
        /// <param name="totalCount">The total count of results.</param>
        /// <param name="totalPages">The total count of pages.</param>
        public PagedResult(IEnumerable<T> results, IEnumerable<PageModel> pages, int totalCount, int totalPages)
        {
            Data = new ReadOnlyCollection<T>(results.ToList());
            Pages = new ReadOnlyCollection<PageModel>(pages.ToList());
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collection of pages.
        /// </summary>
        [JsonProperty(PropertyName = "pages")]
        public IReadOnlyCollection<PageModel> Pages
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of results.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IReadOnlyCollection<T> Data
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total count of results.
        /// </summary>
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total count of pages.
        /// </summary>
        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages
        {
            get;
            private set;
        }

        #endregion
    }
}
