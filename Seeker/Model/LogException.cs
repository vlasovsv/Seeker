namespace Seeker.Model
{
    /// <summary>
    /// Represents an information about exception.
    /// </summary>
    public class LogException
    {
        #region Properties

        /// <summary>
        /// Gets an exception type.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        #endregion
    }
}
