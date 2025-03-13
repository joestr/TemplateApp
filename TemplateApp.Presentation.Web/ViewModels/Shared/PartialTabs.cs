using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialTabs : WebAppComponentViewModel<PartialTabs>
    {
        public List<PartialTab> Tabs { get; set; }
        public string SelectedTabId { get; set; }

        public PartialTabs(string identifier, string refreshUrl, List<WebAppRefreshOnEvent> refreshOnEvents, List<PartialTab> tabs, string selectedTabId) : base(identifier, refreshUrl, refreshOnEvents)
        {
            Tabs = tabs;
            SelectedTabId = selectedTabId;
        }

        public void FillFromQueryCollection(IQueryCollection queryCollection)
        {
            var selectedTabId = queryCollection[Identifier + "_selectedTabId"].FirstOrDefault();

            if (!string.IsNullOrEmpty(selectedTabId))
            {
                SelectedTabId = selectedTabId;
            }
        }
    }
}
