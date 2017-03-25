using System;
using System.Collections.Generic;

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
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a log level.
        /// </summary>
        public LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a log message.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets an exception info.
        /// </summary>
        public LogException Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Gets custom properties.
        /// </summary>
        public IReadOnlyDictionary<string, object> Properties
        {
            get;
            set;
        }

        #endregion
    }
}
