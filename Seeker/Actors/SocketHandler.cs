using System;
using System.Net;
using System.Text;

using Akka.Actor;
using Akka.IO;
using Topshelf.Logging;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to read messages from a socket.
    /// </summary>
    public class SocketHandler : ReceiveActor
    {
        #region Private fields

        private readonly LogWriter _seekerLogger = HostLogger.Get("Seeker");

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="remote">A remote endpoint.</param>
        /// <param name="connection">A connection actor.</param>
        public SocketHandler(EndPoint remote, IActorRef connection)
        {
            Context.Watch(connection);

            Receive<Tcp.Received>(received =>
            {
                var text = Encoding.UTF8.GetString(received.Data.ToArray()).Trim();
                Sender.Tell(Tcp.Write.Create(received.Data));

                Context.ActorSelection(ActorPaths.ProcessorManager.Path).Tell(text);
            });
            Receive<Tcp.ConnectionClosed>(closed =>
            {
                _seekerLogger.Debug(string.Format("The remote host {0} closed the connection.", remote));
                Context.Stop(Self);
            });
            Receive<Terminated>(terminated =>
            {
                _seekerLogger.Debug(string.Format("The remote host {0} terminated.", remote));
                Context.Stop(Self);
            });
        }

        #endregion
    }
}
