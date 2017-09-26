using Akka.Actor;

using Seeker.Messages;
using System.Collections.Generic;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents a user manager.
    /// </summary>
    public sealed class UserManager : ReceiveActor
    {
        private List<string> _loggedUsers;

        /// <summary>
        /// Creates a new user manager.
        /// </summary>
        public UserManager()
        {
            _loggedUsers = new List<string>();

            Receive<UserLoggedInMessage>(msg =>
            {
                if (!_loggedUsers.Contains(msg.UserName))
                {
                    Context.ActorOf<UserActor>(msg.UserName);
                    _loggedUsers.Add(msg.UserName);
                }
                
            });

            Receive<UserLoggedOutMessage>(msg =>
            {
                if (_loggedUsers.Contains(msg.UserName))
                {
                    _loggedUsers.Remove(msg.UserName);
                    var actorPath = new ActorMetaData(msg.UserName, ActorPaths.UserManager);
                    Context.ActorSelection(actorPath.Path).Tell(PoisonPill.Instance);
                }
            });
        }
    }
}
