using Akka.Actor;

using Seeker.Models;

namespace Seeker.Core.Actors
{
    public class StatisticsManager : ReceiveActor
    {
        public StatisticsManager()
        {
            Receive<LogEventData>(log => OnReceiveLog(log));
        }

        private void OnReceiveLog(LogEventData logEventData)
        {
            
        }
    }
}
