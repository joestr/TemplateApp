using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared
{
    public class PartialModalDialogContent : WebAppComponentViewModel<PartialModalDialogContent>
    {
        public static Func<string> ClassName = () =>
        {
            var classNameString = nameof(PartialModalDialogContent);
            var partialLength = "Partial".Length;
            return classNameString.Substring(partialLength, classNameString.Length - partialLength);
        };

        public static Func<string> EventHandlerNameOnOpenedModalDialog =
            () => "onOpenedModalDialog";

        public PartialModalDialogContent()
        {
        }

        public PartialModalDialogContent(
            string identifier,
            string refreshUrl,
            List<WebAppRefreshOnEvent> refreshOnEvents) : base(identifier, refreshUrl, refreshOnEvents)
        {
        }

        public void FillFromQueryCollection(IQueryCollection queryCollection)
        {
        }
    }
}
