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
            IDictionary<string, string> hiddenNameActions) : base(identifier, hiddenNameActions)
        {
        }

        public void FillFromQueryCollection(IQueryCollection queryCollection)
        {
        }
    }
}
