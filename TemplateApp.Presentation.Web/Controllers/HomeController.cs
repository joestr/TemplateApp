using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using TemplateApp.Presentation.Web.Misc;
using TemplateApp.Presentation.Web.ViewModels.Shared;
using TemplateApp.ViewModels;

namespace TemplateApp.Controllers
{
    public class HomeController : Controller
    {
        private const string TabsIdentifier = "Tabs";
        private const string DialogIdentifier = "Dialog";
        private const string DialogContentIdentifier = "DialogContent";
        private const string ButtonIdentifier = "Button";
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return new RedirectToActionResult("Start", "Home", null);
            }

            return View();
        }

        private PartialTabs BuildTabs()
        {
            var result = new PartialTabs(
                identifier: TabsIdentifier,
                hiddenInputNameAndValues: new Dictionary<string, string>() { },
                Request.Query,
                tabs: [],
                selectedTab: null);
            
            result.Tabs.Add(
                new PartialTab(
                    view: "_PartialText",
                    viewModel: new PartialText("Content for tab 1"),
                    id: "tab1",
                    title: "Tab 1"));
            result.Tabs.Add(
                new PartialTab(
                    "_PartialText",
                    new PartialText("Content for tab 2"),
                    "tab2",
                    "Tab 2"));
            result.Tabs.Add(
                new PartialTab(
                    "_PartialText",
                    new PartialText("Content for tab 3"),
                    "tab3",
                    "Tab 3"));
            
            return result;
        }

        private PartialLinkButton BuildLinkButtonWithText()
        {
            return new PartialLinkButton("Button with text", "/", "", "Create", AnchorTarget.Self);
        }

        private PartialButton BuildOpenDialogButton()
        {
            return new PartialButton(
                identifier: ButtonIdentifier,
                hiddenNameAndNamesAnd: new Dictionary<string, string>() { },
                queryCollection: Request.Query,
                title: "Open dialog", 
                icon: "", 
                text: "Open dialog",
                actionName: $"{DialogIdentifier}{nameof(PartialDialog.OpenedDialog)}",
                actionValue: $"{DialogIdentifier}");
        }

        private PartialDialog BuildDialog()
        {
            var result = new PartialDialog(
                DialogIdentifier,
                hiddenInputNameAndValues: new Dictionary<string, string>() { },
                queryCollection: Request.Query,
                title: "Dialog",
                content: BuildDialogContent(),
                openedDialog: null);

            return result;
        }

        private PartialDialogContent BuildDialogContent()
        {
            var result = new PartialDialogContent(
                identifier: DialogContentIdentifier,
                hiddenInputNameAndValues: new Dictionary<string, string>()  { },
                queryCollection: Request.Query);

            return result;
        }

        //[Authorize]
        public IActionResult Start()
        {
            var viewModel = new Start();
            
            viewModel.PartialTabs = BuildTabs();
            viewModel.LinkButtonWithText = BuildLinkButtonWithText();
            viewModel.OpenDialogButton = BuildOpenDialogButton();
            viewModel.Dialog = BuildDialog();

            viewModel.Dialog.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.OpenDialogButton.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.PartialTabs.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });

            viewModel.PartialTabs.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.OpenDialogButton.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Dialog.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });
            
            viewModel.OpenDialogButton.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.PartialTabs.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Dialog.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });

            return View(viewModel);
        }
    }
}
