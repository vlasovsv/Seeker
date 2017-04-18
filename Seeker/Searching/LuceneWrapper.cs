using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Newtonsoft.Json;

using Seeker.Configuration;
using Seeker.Model;
using Lucene.Net.QueryParsers;

namespace Seeker.Searching
{
    /// <summary>
    /// Represents a lucene wrapper.
    /// </summary>
    public class LuceneWrapper
    {
        #region Constructors

        /// <summary>
        /// Creates an instnce of the lucene wrapper.
        /// </summary>
        /// <param name="settings">Seeker settings.</param>
        public LuceneWrapper(ISeekerSettings settings)
        {
            DataFolder = Path.Combine(settings.Store, "Index");
            if (!System.IO.Directory.Exists(DataFolder))
            {
                System.IO.Directory.CreateDirectory(DataFolder);
            }

            IndexDirectory = FSDirectory.Open(DataFolder);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a path to an index floder.
        /// </summary>
        public string DataFolder
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a lucene index directory.
        /// </summary>
        public FSDirectory IndexDirectory
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks if the index exists.
        /// </summary>
        public bool IsIndexExisted
        {
            get
            {
                return IndexReader.IndexExists(IndexDirectory);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a document to index.
        /// </summary>
        /// <param name="document">A document.</param>
        public void AddDocument(Document document)
        {
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            using (var writer = new IndexWriter(IndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.AddDocument(document);
            }
        }

        /// <summary>
        /// Adds documents to index.
        /// </summary>
        /// <param name="documents">Documents.</param>
        public void AddDocuments(IEnumerable<Document> documents)
        {
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            using (var writer = new IndexWriter(IndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var doc in documents)
                {
                    writer.AddDocument(doc);
                }
            }
        }

        /// <summary>
        /// Adds an item to index.
        /// </summary>
        /// <typeparam name="T">An item type.</typeparam>
        /// <param name="item">An intem.</param>
        /// <param name="mapper">An item-document func.</param>
        public void AddItem<T>(T item, Func<T, Document> mapper)
        {
            var doc = mapper(item);
            AddDocument(doc);
        }

        /// <summary>
        /// Adds items to index.
        /// </summary>
        /// <typeparam name="T">An item type.</typeparam>
        /// <param name="items">Items.</param>
        /// <param name="mapper">An item-document func.</param>
        public void AddItems<T>(IEnumerable<T> items, Func<T, Document> mapper)
        {
            var documents = items.Select(x => mapper(x)).ToArray();
            AddDocuments(documents);
        }

        /// <summary>
        /// Searches log items.
        /// </summary>
        /// <param name="request">Search request.</param>
        /// <returns>
        /// Returns a collection of log items.
        /// </returns>
        public IEnumerable<LogEventData> Search(SearchRequest request)
        {
            if (IsIndexExisted)
            {
                using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
                {
                    using (var searcher = new IndexSearcher(IndexDirectory, true))
                    {
                        var dateQuery = new TermRangeQuery("Timestamp",
                            DateTools.DateToString(request.StartDate, DateTools.Resolution.MILLISECOND),
                            DateTools.DateToString(request.EndDate, DateTools.Resolution.MILLISECOND),
                            true,
                            true);

                        var filter = new BooleanQuery();
                        filter.Add(dateQuery, Occur.MUST);

                        if (!request.IsQueryEmpty)
                        {
                            var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Message", analyzer);
                            var query = parser.Parse(request.Query);
                            filter.Add(query, Occur.MUST);
                        }

                        var hits = searcher.Search(filter, 10000);
                        List<LogEventData> results = new List<LogEventData>(hits.TotalHits);
                        foreach (var scoreDoc in hits.ScoreDocs)
                        {
                            var doc = searcher.Doc(scoreDoc.Doc);
                            var raw = doc.Get("Raw");
                            var logEvent = JsonConvert.DeserializeObject<LogEventData>(raw);
                            results.Add(logEvent);
                        }
                        return results;
                    }
                }
            }
            else
            {
                return Enumerable.Empty<LogEventData>();
            }
        }

        #endregion
    }
}
