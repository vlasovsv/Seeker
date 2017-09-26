using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seeker.Models
{
    /// <summary>
    /// Represents a log event.
    /// </summary>
    [Serializable]
    public class LogEventData
    {
        #region Constructors

        /// <summary>
        /// Creates a new log event.
        /// </summary>
        /// <param name="timestamp">The log timestamp.</param>
        /// <param name="level">The log level.</param>
        /// <param name="message">The log message.</param>
        /// <param name="exception">The log exception.</param>
        /// <param name="properties">The log additional properties.</param>
        public LogEventData(DateTime timestamp, LogLevel level, string message, LogException exception, ReadOnlyDictionary<string, object> properties)
        {
            Timestamp = timestamp;
            Level = level;
            Message = message;
            Exception = exception;
            Properties = properties;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a log timestamp.
        /// </summary>
        [JsonProperty(PropertyName ="timestamp")]
        public DateTime Timestamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a log level.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "level")]
        public LogLevel Level
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a log message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets an exception info.
        /// </summary>
        [JsonProperty(PropertyName = "exception")]
        public LogException Exception
        {
            get;
            private set;
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
        public ReadOnlyDictionary<string, object> Properties
        {
            get;
            private set;
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
