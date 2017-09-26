using System;

namespace Seeker.Models
{
    /// <summary>
    /// Represents a page model for pagination.
    /// </summary>
    public class PageModel
    {
        #region Constructors

        /// <summary>
        /// Creates a new page model.
        /// </summary>
        /// <param name="name">Displayed name.</param>
        /// <param name="link">Url.</param>
        /// <param name="isCurrent">The value indicating whether this page is current.</param>
        /// <param name="isGap">The value indicating whether this page is a gap.</param>
        public PageModel(string name, Uri link, bool isCurrent, bool isGap = false)
        {
            Name = name;
            Link = link;
            IsCurrent = isCurrent;
            IsGap = isGap;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the url to this page.
        /// </summary>
        public Uri Link
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating whether this page is current.
        /// </summary>
        public bool IsCurrent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating whether this page is a gap.
        /// </summary>
        public bool IsGap
        {
            get;
            private set;
        } 

        #endregion
    }
}
