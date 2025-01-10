namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialRefreshTable
    {
        public string Identifier { get; }
        public ICollection<IDictionary<string, object>> Entries { get; set; }
        public int CountAll { get; set; }
        public int Count { get; set; }
        public string RefreshEntriesUri { get; set; }
        public bool ShowTableHeader { get; set; }
        public bool ShowTableFooter { get; set; }

        public string? SearchTerm { get; set; }
        public int PerPageItemCount { get; set; }
        public int Page { get; set; }

        public int Pages => (int)Math.Ceiling(Count / (decimal)PerPageItemCount);

        public bool IsFilterActive => !string.IsNullOrEmpty(SearchTerm);
        public bool IsEmpty => Count == 0;

        public PartialRefreshTable(string identifier, ICollection<IDictionary<string, object>> entries, int countAll, int count, string refreshEntriesUri, string? searchTerm = null, int perPageItemCount = 5, int page = 1, bool showTableHeader = true, bool showTableFooter = false)
        {
            Identifier = identifier;
            Entries = entries;
            CountAll = countAll;
            Count = count;
            RefreshEntriesUri = refreshEntriesUri;
            SearchTerm = searchTerm;
            PerPageItemCount = perPageItemCount;
            Page = page;
            ShowTableHeader = showTableHeader;
            ShowTableFooter = showTableFooter;
        }

        public void FillFromQueryCollection(IQueryCollection queryCollection)
        {
            this.SearchTerm = queryCollection[this.Identifier + "_searchTerm"].FirstOrDefault();

            var perPageItemCount = queryCollection[this.Identifier + "_perPageItemCount"].FirstOrDefault();
            this.PerPageItemCount = perPageItemCount != null ? int.Parse(perPageItemCount) : 5;

            var page = queryCollection[this.Identifier + "_page"].FirstOrDefault();
            this.Page = page != null ? int.Parse(page) : 1;

            /* Set current page to max page if over */
            if (this.Page > this.Pages)
            {
                this.Page = Pages;
            }

            /* Set current page to 1 (one) if less thn 1 (one) */
            if (this.Page < 1)
            {
                this.Page = 1;
            }
        }
    }
}
