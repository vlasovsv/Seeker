using System;

using Newtonsoft.Json;

namespace Seeker.Model
{
    /// <summary>
    /// Represents an information about exception.
    /// </summary>
    [Serializable]
    public class LogException
    {
        #region Properties

        /// <summary>
        /// Gets an exception type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get;
            set;
        }

        #endregion
    }
}
