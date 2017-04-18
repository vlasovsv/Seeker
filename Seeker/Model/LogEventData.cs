using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seeker.Model
{
    /// <summary>
    /// Represents a log event.
    /// </summary>
    public class LogEventData
    {
        #region Properties

        /// <summary>
        /// Gets a log timestamp.
        /// </summary>
        [JsonProperty(PropertyName ="timestamp")]
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a log level.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "level")]
        public LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a log message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets an exception info.
        /// </summary>
        [JsonProperty(PropertyName = "exception")]
        public LogException Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Gets custom properties.
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public IReadOnlyDictionary<string, object> Properties
        {
            get;
            set;
        }

        #endregion
    }
}
