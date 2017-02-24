using System.Linq;

using Akka.Actor;
using Lucene.Net.Documents;
using Newtonsoft.Json.Linq;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to create a document for being indexed.
    /// </summary>
    public class DocumentMapper : ReceiveActor
    {
        #region Constructors

        /// <summary>
        /// Creates a document mapper actor.
        /// </summary>
        public DocumentMapper()
        {
            Receive<JObject>(obj =>
            {
                ProcessObject(obj);
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes a json-object.
        /// </summary>
        /// <param name="obj">A json-object instance.</param>
        private void ProcessObject(JObject obj)
        {
            var document = new Document();
            var properties = obj.Descendants().OfType<JProperty>();

            foreach (var property in properties)
            {
                if (!property.Children<JObject>().Any())
                {
                    var value = property.Value;
                    document.Add(new Field(property.Name, value.Value<string>(), Field.Store.YES, Field.Index.ANALYZED));
                }
            }
            document.Add(new Field("raw", obj.ToString(), Field.Store.YES, Field.Index.NO));

            Context.ActorSelection(string.Format("/user/{0}", ActorNames.Indexer)).Tell(document);
        }

        #endregion
    }
}
