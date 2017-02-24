using System.Net;

using Akka.Actor;
using Akka.IO;

using Topshelf.Logging;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to listen a socket connection.
    /// </summary>
    public class SocketListener : ReceiveActor
    {
        #region Private fields

        private readonly IActorRef _manager = Context.System.Tcp();
        private readonly LogWriter _seekerLogger = HostLogger.Get("Seeker");

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a listener.
        /// </summary>
        /// <param name="endpoint">An endpoint.</param>
        public SocketListener(EndPoint endpoint)
        {
            _manager.Tell(new Tcp.Bind(Self, endpoint));

            Receive<Tcp.Connected>(connected =>
            {
                _seekerLogger.Debug(string.Format("Принято соединение от {0}", connected.RemoteAddress));
                Sender.Tell(new Tcp.Register(Context.ActorOf(Props.Create(() => new SocketHandler(connected.RemoteAddress, Sender)))));
            });
        }

        #endregion
    }
}
