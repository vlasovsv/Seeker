using System;

namespace Seeker.Model
{
    public class PageModel
    {
        public PageModel(string name, Uri link, bool isCurrent, bool isGap = false)
        {
            Name = name;
            Link = link;
            IsCurrent = isCurrent;
            IsGap = isGap;
        }

        public string Name
        {
            get;
            private set;
        }

        public Uri Link
        {
            get;
            private set;
        }

        public bool IsCurrent
        {
            get;
            private set;
        }

        public bool IsGap
        {
            get;
            private set;
        }
    }
}
