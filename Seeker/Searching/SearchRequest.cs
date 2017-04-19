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
        /// Creates a new search request.
        /// </summary>
        /// <param name="query">A search query.</param>
        /// <param name="startDate">A start date of serching.</param>
        /// <param name="endDate">An end date of searching.</param>
        public SearchRequest(string query, DateTime startDate, DateTime endDate)
        {
            var upper = endDate;
            var lower = startDate;
            StartDate = lower;
            EndDate = upper;
            Query = query;
        }

        /// <summary>
        /// Creates a new search request.
        /// </summary>
        /// <param name="query">A search query.</param>
        public SearchRequest(string query)
            : this(query, DateTime.Now.AddDays(-7), DateTime.Now)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a start date.
        /// </summary>
        public DateTime StartDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets an end date.
        /// </summary>
        public DateTime EndDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a query.
        /// </summary>
        public string Query
        {
            get;
            private set;
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
