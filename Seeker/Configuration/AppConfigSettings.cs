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
        /// Gets or sets a http api port.
        /// </summary>
        public int Port
        {
            get
            {
                return _seekerSection.Port;
            }
            set
            {
                _seekerSection.Port = value;
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

        #endregion
    }
}
