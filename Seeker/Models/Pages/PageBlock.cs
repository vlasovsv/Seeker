using System.Linq;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Seeker.Models
{
    /// <summary>
    /// Represents a countainer for pages.
    /// </summary>
    public class PageBlock
    {
        #region Constructors

        /// <summary>
        /// Creates a new container.
        /// </summary>
        /// <param name="totalPages">The total count of pages.</param>
        /// <param name="pages">The collection of pages.</param>
        public PageBlock(int totalPages, IEnumerable<PageModel> pages)
        {
            TotalPages = totalPages;
            Pages = new ReadOnlyCollection<PageModel>(pages.ToList());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the total count of pages.
        /// </summary>
        public int TotalPages
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of pages.
        /// </summary>
        public IReadOnlyCollection<PageModel> Pages
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the first page.
        /// </summary>
        public PageModel First
        {
            get
            {
                return Pages.FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the last page.
        /// </summary>
        public PageModel Last
        {
            get
            {
                return Pages.LastOrDefault();
            }
        }

        #endregion
    }
}
