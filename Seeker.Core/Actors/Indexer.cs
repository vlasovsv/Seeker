using Akka.Actor;
using Lucene.Net.Documents;

using Seeker.Searching;

namespace Seeker.Actors
{
    /// <summary>
    /// Represents an actor that allows to index a document.
    /// </summary>
    public class Indexer : ReceiveActor
    {
        #region Private fields

        private readonly LuceneWrapper _lucene;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a index actor.
        /// </summary>
        /// <param name="lucene">An instance of lucene wrapper.</param>
        public Indexer(LuceneWrapper lucene)
        {
            _lucene = lucene;

            Receive<Document>(doc =>
            {
                lucene.AddDocument(doc);
            });
        }

        #endregion
    }
}
