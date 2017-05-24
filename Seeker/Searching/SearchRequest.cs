using System;

namespace Seeker.Searching
{
    /// <summary>
    /// Represents a search request to Seeker.
    /// </summary>
    public class SearchRequest
    {
        #region Constructors

        /// <summary>
        /// Creates a search request.
        /// </summary>
        public SearchRequest()
        {
            Limit = 100;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a query.
        /// </summary>
        public string Query
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the query is empty.
        /// </summary>
        public bool IsQueryEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Query);
            }
        }

        /// <summary>
        /// Gets or sets offset.
        /// </summary>
        public int Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets limit.
        /// </summary>
        public int Limit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets value that means a server will return always 200 OK code if an exception occures.
        /// </summary>
        public bool SuppressResponseCodes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a field that will be used as a sort key.
        /// </summary>
        public string OrderBy
        {
            get;
            set;
        }

        #endregion
    }
}
