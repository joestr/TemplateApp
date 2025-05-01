using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialDialogContent : WebAppComponentViewModel
    {
        public PartialDialogContent()
        {
        }

        public PartialDialogContent(
            string identifier,
            IDictionary<string, string> hiddenInputNameAndValues,
            IQueryCollection queryCollection) : base(identifier, hiddenInputNameAndValues, queryCollection)
        {
            FillFromQuery();
        }

        public override void FillFromQuery(IQueryCollection queryCollection)
        {
        }

        public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
        {
            var result = new List<KeyValuePair<string, string>>();
            return result;
        }
    }
}
