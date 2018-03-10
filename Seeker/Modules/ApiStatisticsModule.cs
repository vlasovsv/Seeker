using System;
using System.Linq;

using Nancy;

using Seeker.Searching;

namespace Seeker.Modules
{
    public sealed class ApiStatisticsModule : NancyModule
    {
        public ApiStatisticsModule(LuceneWrapper lucene)
            : base("api/v1/")
        {
            Get("/stats", parameters =>
            {
                var request = new SearchRequest();
                var to = DateTime.Parse("08.01.2018").AddDays(1);
                var from = to.AddMonths(-5);
                request.Query = string.Format("Timestamp:[{0} TO {1}]", from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"));
                request.Limit = 1000;

                var logs = lucene.Search(request);

                var result = logs.Data
                    .GroupBy(x => x.Timestamp.Date)
                    .Select(x => new
                    {
                        Date = x.Key.ToString("yyyyMMdd"),
                        Statistics = x.ToLookup(y => y.Level)
                            .ToDictionary(y => y.Key.ToString(), y => y.Count())
                    })
                    .ToArray();

                return Response.AsJson(result);
            });
        }
    }
}
