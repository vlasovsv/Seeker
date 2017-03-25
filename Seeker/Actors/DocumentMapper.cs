using System;
using System.Collections.Generic;
using System.Linq;

using Akka.Actor;
using Lucene.Net.Documents;
using Newtonsoft.Json.Linq;
using Seeker.Model;
using Newtonsoft.Json;

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
            Receive<LogEventData>(logEvent =>
            {
                ProcessObject(logEvent);
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes a json-object.
        /// </summary>
        /// <param name="logEvent">Log event data.</param>
        private void ProcessObject(LogEventData logEvent)
        {
            var doc = new Document();

            doc.Add(new Field("Timestamp", DateTools.DateToString(logEvent.Timestamp, DateTools.Resolution.SECOND), Field.Store.NO, Field.Index.ANALYZED));
            doc.Add(new Field("Level", logEvent.Level.ToString(), Field.Store.NO, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Message", logEvent.Message, Field.Store.NO, Field.Index.ANALYZED));

            ProcessException(logEvent.Exception, doc);
            ProcessProperties(logEvent.Properties, doc);

            doc.Add(new Field("Raw", JsonConvert.SerializeObject(logEvent), Field.Store.YES, Field.Index.NO));

            Context.ActorSelection(ActorPaths.Indexer.Path).Tell(doc);
        }

        private void ProcessException(LogException exception, Document doc)
        {
            if (exception != null)
            {
                doc.Add(new Field("ExceptionType", exception.Type, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("ExceptionMessage", exception.Message, Field.Store.YES, Field.Index.ANALYZED));
            }
        }

        private void ProcessProperties(IReadOnlyDictionary<string, object> properties, Document doc)
        {
            if (properties != null)
            {
                foreach (var kvp in properties)
                {
                    doc.Add(new Field(kvp.Key, kvp.Value.ToString(), Field.Store.NO, Field.Index.ANALYZED));
                }
            }
        }

        #endregion
    }
}
