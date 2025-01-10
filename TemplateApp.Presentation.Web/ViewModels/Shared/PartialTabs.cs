using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialTabs : WebAppComponentViewModel<PartialTabs>
    {
        public List<PartialTab> Tabs { get; set; } = null;
        public string SelectedTabsId { get; set; } = "";
        public string SelectedTabId { get; set; } = "";

        public PartialTabs()
        {
            
        }

        public PartialTabs(List<PartialTab> tabs, string identifier, string refreshUrl, List<WebAppRefreshOnEvent> refreshOnEvents, string selectedTabsId, string selectedTabId) : base(identifier, refreshUrl, refreshOnEvents)
        {
            Tabs = tabs;
            Identifier = identifier;
            RefreshUrl = refreshUrl;
            RefreshOnEvents = refreshOnEvents;
            SelectedTabsId = selectedTabsId;
            SelectedTabId = selectedTabId;
        }

        public void FillFromQueryCollection(IQueryCollection queryCollection)
        {
            var selectedTabsId = queryCollection[Identifier + "_selectedTabsId"].FirstOrDefault();

            if (!string.IsNullOrEmpty(selectedTabsId))
            {
                SelectedTabsId = selectedTabsId;
            }

            var selectedTabId = queryCollection[Identifier + "_selectedTabId"].FirstOrDefault();

            if (!string.IsNullOrEmpty(selectedTabId))
            {
                SelectedTabId = selectedTabId;
            }
        }
    }
}
