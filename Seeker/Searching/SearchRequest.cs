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
        /// Creates a new request.
        /// </summary>
        public SearchRequest()
        {
            var upper = DateTime.Now;
            var lower = upper.AddDays(-7);
            StartDate = lower;
            EndDate = upper;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a start date.
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an end date.
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

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

        #endregion
    }
}
