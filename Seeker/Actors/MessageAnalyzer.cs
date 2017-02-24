using System;

using Akka.Actor;
using Akka.DI.Core;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to analyze a message.
    /// </summary>
    public class MessageAnalyzer : ReceiveActor
    {
        #region Constructors

        /// <summary>
        /// Creates a message parser actor.
        /// </summary>
        public MessageAnalyzer()
        {
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
            var messages = message.Replace("}{", "}\n{").Split(new string[] { "\n" }, StringSplitOptions.None);
            foreach (var msg in messages)
            {
                Context.ActorOf(Context.System.DI().Props<MessageProcessor>())
                    .Tell(msg);
            }
        }

        #endregion
    }
}
