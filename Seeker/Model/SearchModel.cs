using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Seeker.Model
{
    /// <summary>
    /// Represents a search page model.
    /// </summary>
    public class SearchModel
    {
        #region Private fields

        private readonly ReadOnlyCollection<PageModel> _pages;
        private readonly ReadOnlyCollection<LogEventData> _results;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new seach page model.
        /// </summary>
        /// <param name="results">The search result collection.</param>
        /// <param name="pages">The pagination collection.</param>
        public SearchModel(IEnumerable<LogEventData> results, IEnumerable<PageModel> pages)
        {
            _results = new ReadOnlyCollection<LogEventData>(results.ToList());
            _pages = new ReadOnlyCollection<PageModel>(pages.ToList());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a pagination collection.
        /// </summary>
        public IReadOnlyCollection<PageModel> Pages
        {
            get
            {
                return _pages;
            }
        }

        /// <summary>
        /// Gets a search result collection.
        /// </summary>
        public IReadOnlyCollection<LogEventData> Results
        {
            get
            {
                return _results;
            }
        }

        #endregion
    }
}
