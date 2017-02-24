using System.Configuration;

namespace Seeker.Configuration
{
    /// <summary>
    /// Seeker settings based on App.Config.
    /// </summary>
    public class AppConfigSettings : ISeekerSettings
    {
        #region Constants

        public const string SEEKER_SECTION = "seeker";

        #endregion

        #region Private fields

        private readonly SeekerSection _seekerSection;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates seeker settings based App.Config.
        /// </summary>
        public AppConfigSettings()
        {
            _seekerSection = ConfigurationManager.GetSection(SEEKER_SECTION) as SeekerSection;

            if (_seekerSection == null)
            {
                _seekerSection = new SeekerSection();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a socket port.
        /// </summary>
        public int TcpPort
        {
            get
            {
                return _seekerSection.TcpPort;
            }
            set
            {
                _seekerSection.TcpPort = value;
            }
        }

        /// <summary>
        /// Gets or sets a http api port.
        /// </summary>
        public int HttpApiPort
        {
            get
            {
                return _seekerSection.HttpApiPort;
            }
            set
            {
                _seekerSection.HttpApiPort = value;
            }
        }

        /// <summary>
        /// Gets or sets a path to the store.
        /// </summary>
        public string Store
        {
            get
            {
                return _seekerSection.Store;
            }
            set
            {
                _seekerSection.Store = value;
            }
        }

        /// <summary>
        /// Gets or sets a raw log format.
        /// </summary>
        public LogFormatting Formatting
        {
            get
            {
                return _seekerSection.Formatting;
            }
            set
            {
                _seekerSection.Formatting = value;
            }
        }

        #endregion
    }
}
