using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialTabs : WebAppComponentViewModel
    {
        public List<PartialTab> Tabs { get; set; }
        public string SelectedTabId { get; set; }

        public PartialTabs(string identifier, IDictionary<string, string> hiddenNameActions, List<PartialTab> tabs, string selectedTabId) : base(identifier, hiddenNameActions)
        {
            Tabs = tabs;
            SelectedTabId = selectedTabId;
        }

        public void FillFromQueryCollection(IQueryCollection queryCollection)
        {
            var selectedTabId = queryCollection[Identifier + nameof(SelectedTabId)].FirstOrDefault();

            if (!string.IsNullOrEmpty(selectedTabId))
            {
                SelectedTabId = selectedTabId;
            }
        }
    }
}
