using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialTabs : WebAppComponentViewModel
    {
        public List<PartialTab> Tabs { get; set; }
        public string? SelectedTab { get; set; }

        public PartialTabs(string identifier, IDictionary<string, string> hiddenInputNameAndValues, IQueryCollection queryCollection, List<PartialTab> tabs, string? selectedTab) : base(identifier, hiddenInputNameAndValues, queryCollection)
        {
            Tabs = tabs;
            SelectedTab = selectedTab;
            FillFromQuery();
        }

        public override void FillFromQuery(IQueryCollection queryCollection)
        {
            SelectedTab = GetStringFromQueryParameter(queryCollection, nameof(SelectedTab));
        }

        public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
        {
            var result = new List<KeyValuePair<string, string>>();
            result = this.AddNullableToList(result, this.GetHiddenInputNameAndValue(nameof(SelectedTab), SelectedTab));
            return result;
        }
    }
}
