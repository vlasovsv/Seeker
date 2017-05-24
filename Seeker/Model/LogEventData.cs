using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seeker.Model
{
    /// <summary>
    /// Represents a log event.
    /// </summary>
    [Serializable]
    public class LogEventData
    {
        #region Properties

        /// <summary>
        /// Gets or sets a log timestamp.
        /// </summary>
        [JsonProperty(PropertyName ="timestamp")]
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a log level.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "level")]
        public LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a log message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an exception info.
        /// </summary>
        [JsonProperty(PropertyName = "exception")]
        public LogException Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Gets value that represents if log contains an exception or not.
        /// </summary>
        [JsonProperty(PropertyName = "has_exception")]
        public bool HasException
        {
            get
            {
                return Exception != null;
            }
        }

        /// <summary>
        /// Gets custom properties.
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string, object> Properties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets value that represents if log contains properties or not.
        /// </summary>
        [JsonProperty(PropertyName = "has_properties")]
        public bool HasProperties
        {
            get
            {
                return Properties != null;
            }
        }

        #endregion
    }
}
