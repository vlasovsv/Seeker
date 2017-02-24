using Akka.Actor;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents a supervisor actor that allows to monitor message parser actors and route parse request.
    /// </summary>
    public class ProcessorManager : ReceiveActor
    {
        #region Constructors

        /// <summary>
        /// Creates a supervisor actor.
        /// </summary>
        public ProcessorManager()
        {
            Receive<string>(msg =>
            {
                var processor = Context.ActorOf<MessageAnalyzer>();
                processor.Tell(msg);
            });
        }

        #endregion
    }
}
