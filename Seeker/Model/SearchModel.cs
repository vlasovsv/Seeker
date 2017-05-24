using System;
using System.Collections.Generic;

namespace Seeker.Model
{
    public class SearchModel
    {
        public IEnumerable<PageModel> Pages
        {
            get;
            set;
        }

        public IEnumerable<LogEventData> Results
        {
            get;
            set;
        }
    }
}
