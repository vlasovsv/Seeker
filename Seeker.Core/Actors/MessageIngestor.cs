using Akka.Actor;
using Akka.Routing;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to analyze a message.
    /// </summary>
    public class MessageIngestor : ReceiveActor
    {
        #region Private fields

        private IActorRef _router;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a message ingestor actor.
        /// </summary>
        public MessageIngestor()
        {
            _router = Context.ActorOf(Props.Create<MessageProcessor>()
                .WithRouter(FromConfig.Instance), "message-processor-router");
            Receive<string>(msg => AnalyzeMessage(msg));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Analyzes a message.
        /// </summary>
        /// <param name="message">A message.</param>
        private void AnalyzeMessage(string message)
        {
            if (message.StartsWith("["))
            {
                _router.Tell(new MessageProcessor.BatchLog(message));
            }
            else
            {
                _router.Tell(new MessageProcessor.SingleLog(message));
            }
        }

        #endregion
    }
}
