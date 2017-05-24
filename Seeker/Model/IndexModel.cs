namespace Seeker.Model
{
    /// <summary>
    /// Represents an index page model.
    /// </summary>
    public class IndexModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets an array of logs to display.
        /// </summary>
        public LogEventData[] Results
        {
            get;
            set;
        } 

        #endregion
    }
}
