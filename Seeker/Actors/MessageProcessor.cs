using Akka.Actor;
using Newtonsoft.Json;
using Topshelf.Logging;

using Seeker.Configuration;
using Seeker.Model;
using Newtonsoft.Json.Linq;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to parse a message.
    /// </summary>
    public class MessageProcessor : ReceiveActor
    {
        #region Private fields

        private readonly LogWriter _keeperLogger = HostLogger.Get("Keeper");
        private readonly ISeekerSettings _settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a message processor actor.
        /// </summary>
        public MessageProcessor(ISeekerSettings settings)
        {
            _settings = settings;

            Receive<string>(msg =>
            {
                var logEventData = JsonConvert.DeserializeObject<LogEventData>(msg);
                var obj = JObject.Parse(msg);
                var log = obj.ToString((Formatting)_settings.Formatting);
                _keeperLogger.Info(log);

                Context.ActorOf<DocumentMapper>().Tell(logEventData);
            });
        }

        #endregion
    }
}