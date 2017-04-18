using System.IO;

using Nancy;

using Seeker.Actors;

namespace Seeker.Modules
{
    public class LogsModule : NancyModule
    {
        public LogsModule()
            :base("api/v1/")
        {
            Post("/logs", parameters =>
            {
                using (var sr = new StreamReader(Request.Body))
                {
                    var str = sr.ReadToEnd();
                    SeekerContext.ActorSystem.ActorSelection(ActorPaths.MessageIngestor.Path)
                        .Tell(str);
                }
                return HttpStatusCode.OK;
            });
        }
    }
}
