using Akka.Actor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Topshelf.Logging;

using Seeker.Configuration;

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
                var obj = JObject.Parse(msg);
                _keeperLogger.Info(obj.ToString((Formatting)_settings.Formatting));
                Context.ActorOf<DocumentMapper>().Tell(obj);
            });
        }

        #endregion
    }
}