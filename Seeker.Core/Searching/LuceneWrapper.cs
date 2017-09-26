using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Newtonsoft.Json;

using Seeker.Models;

namespace Seeker.Searching
{
    /// <summary>
    /// Represents a lucene wrapper.
    /// </summary>
    public class LuceneWrapper
    {
        #region Private fields

        private readonly Sort _defaultSort;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instnce of the lucene wrapper.
        /// </summary>
        /// <param name="storePath">Store path.</param>
        public LuceneWrapper(string storePath)
        {
            _defaultSort = new Sort(new SortField("Timestamp", SortField.STRING, true));
            DataFolder = Path.Combine(storePath, "Index");
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
        /// Returns a search result.
        /// </returns>
        public SearchResult Search(SearchRequest request)
        {
            if (IsIndexExisted)
            {
                using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
                {
                    using (var searcher = new IndexSearcher(IndexDirectory, true))
                    {
                        var query = new BooleanQuery();

                        if (!request.IsQueryEmpty)
                        {
                            var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Message", analyzer);
                            var contentQuery = parser.Parse(request.Query);
                            query.Add(contentQuery, Occur.MUST);
                        }

                        var filter = new QueryWrapperFilter(query);

                        var order = GetOrderByOrDefault(request.OrderBy);
                        var hits = searcher.Search(query, filter, 500000, order);
                        var limitDocs = hits.ScoreDocs.Skip(request.Offset).Take(request.Limit);
                        List<LogEventData> results = new List<LogEventData>(hits.TotalHits);
                        foreach (var scoreDoc in limitDocs)
                        {
                            var doc = searcher.Doc(scoreDoc.Doc);
                            var raw = doc.Get("Raw");
                            var logEvent = JsonConvert.DeserializeObject<LogEventData>(raw);
                            results.Add(logEvent);
                        }
                        return new SearchResult(results, hits.TotalHits);
                    }
                }
            }
            else
            {
                return SearchResult.Empty;
            }
        }

        private Sort GetOrderByOrDefault(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return _defaultSort;
            }
            else
            {
                var parts = orderBy.Split('|');
                List<SortField> fields = new List<SortField>();
                foreach (var part in parts)
                {
                    var fieldName = part;
                    bool desc = false;
                    if (part.StartsWith("-"))
                    {
                        desc = true;
                        fieldName = part.TrimStart('-');
                    }

                    fields.Add(new SortField(fieldName, SortField.STRING, desc));
                }
                return new Sort(fields.ToArray());
            }
        }

        #endregion
    }
}
