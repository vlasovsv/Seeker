using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Model
{
    public class SearchResult
    {
        #region Private fields

        private static SearchResult _empty = new SearchResult();

        #endregion

        #region Constructors

        private SearchResult()
        {
            Data = new LogEventData[] { };
        }

        public SearchResult(IEnumerable<LogEventData> data, int totalCount)
        {
            Data = data.ToArray();
            TotalCount = totalCount;
        }

        #endregion

        #region Properties

        public static SearchResult Empty
        {
            get
            {
                return _empty;
            }
        }

        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount
        {
            get;
            private set;
        }

        [JsonProperty(PropertyName = "data")]
        public LogEventData[] Data
        {
            get;
            private set;
        }

        #endregion
    }
}
