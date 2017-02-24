namespace Seeker.Configuration
{
    /// <summary>
    /// Seeker settings.
    /// </summary>
    public interface ISeekerSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets a socket port.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets a path to the store.
        /// </summary>
        string Store { get; set; }

        /// <summary>
        /// Gets or sets a raw log format.
        /// </summary>
        LogFormatting Formatting { get; set; }

        #endregion
    }
}
