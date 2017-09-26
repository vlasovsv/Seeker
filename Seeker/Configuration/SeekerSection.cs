using System.Configuration;

namespace Seeker.Configuration
{
    /// <summary>
    /// Represents a configuration section for seeker settings.
    /// </summary>
    public class SeekerSection : ConfigurationSection
    {
        #region Properties

        /// <summary>
        /// Gets or sets a http api port.
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 8080)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a path to the store.
        /// </summary>
        [ConfigurationProperty("store", DefaultValue = @".\Extents")]
        public string Store
        {
            get
            {
                return (string)this["store"];
            }
            set
            {
                this["store"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a path to the security directory.
        /// </summary>
        [ConfigurationProperty("security", DefaultValue = @".\Security")]
        public string Security
        {
            get
            {
                return (string)this["security"];
            }
            set
            {
                this["security"] = value;
            }
        }

        #endregion
    }
}
