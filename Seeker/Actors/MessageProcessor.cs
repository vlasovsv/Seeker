using Akka.Actor;
using Newtonsoft.Json;
using Topshelf.Logging;

using Seeker.Model;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to parse a message.
    /// </summary>
    public class MessageProcessor : ReceiveActor
    {
        #region Actor messages

        public class SingleLog
        {
            #region Private fields

            private readonly string _rawLog;

            #endregion

            #region Constructors

            /// <summary>
            /// Creates a single log message.
            /// </summary>
            /// <param name="rawLog"></param>
            public SingleLog(string rawLog)
            {
                _rawLog = rawLog;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets a raw log.
            /// </summary>
            public string RawLog
            {
                get
                {
                    return _rawLog;
                }
            }

            #endregion
        }

        public class BatchLog
        {
            #region Private fields

            private readonly string _rawLog;

            #endregion

            #region Constructors

            /// <summary>
            /// Creates a batch log message.
            /// </summary>
            /// <param name="rawLog"></param>
            public BatchLog(string rawLog)
            {
                _rawLog = rawLog;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets a raw log.
            /// </summary>
            public string RawLog
            {
                get
                {
                    return _rawLog;
                }
            }

            #endregion
        }

        #endregion

        #region Private fields

        private readonly LogWriter _keeperLogger = HostLogger.Get("Keeper");

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a message processor actor.
        /// </summary>
        public MessageProcessor()
        {
            Receive<SingleLog>(msg =>
            {
                var logEventData = JsonConvert.DeserializeObject<LogEventData>(msg.RawLog);
                Context.ActorOf<DocumentMapper>().Tell(logEventData);
            });

            Receive<BatchLog>(msg =>
            {
                var logEvents = JsonConvert.DeserializeObject<LogEventData[]>(msg.RawLog);
                foreach (var logEventData in logEvents)
                {
                    Context.ActorOf<DocumentMapper>().Tell(logEventData);
                }
            });
        }

        #endregion
    }
}