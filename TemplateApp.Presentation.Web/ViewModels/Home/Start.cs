using TemplateApp.Presentation.Web.ViewModels.Shared;

namespace TemplateApp.Presentation.Web.ViewModels.Home
{
    public class Start
    {
        public PartialTable Table { get; set; }
        public PartialTabs PartialTabs { get; set; }
        public PartialLinkButton LinkButtonWithText { get; set; }
        public PartialButton OpenDialogButton { get; set; }
        public PartialDialog Dialog { get; set; }
        public PartialTree Tree { get; set; }
    }
}
