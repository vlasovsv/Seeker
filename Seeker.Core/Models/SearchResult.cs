using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Seeker.Models
{
    /// <summary>
    /// Represents an API search result.
    /// </summary>
    public class SearchResult
    {
        #region Private fields

        private static SearchResult _empty = new SearchResult();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new empty search result.
        /// </summary>
        private SearchResult()
        {
            Data = new LogEventData[] { };
        }

        /// <summary>
        /// Creates a search result.
        /// </summary>
        /// <param name="data">The log collection.</param>
        /// <param name="totalCount">The total count of logs.</param>
        public SearchResult(IEnumerable<LogEventData> data, int totalCount)
        {
            Data = data.ToArray();
            TotalCount = totalCount;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an empty result.
        /// </summary>
        public static SearchResult Empty
        {
            get
            {
                return _empty;
            }
        }

        /// <summary>
        /// Gets the total count of logs.
        /// </summary>
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the result collection of searching.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public LogEventData[] Data
        {
            get;
            private set;
        }

        #endregion
    }
}
