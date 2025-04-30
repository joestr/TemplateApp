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
                "tabs",
                new Dictionary<string, string>()
                {
                },
                [],
                null);
            result.FillFromQueryCollection(Request.Query);
            
            result.Tabs.Add(
                new PartialTab(
                    "_PartialText",
                    new PartialText("Content for tab 1"),
                    "tab1",
                    "Tab 1"));
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
                "btn",
                new Dictionary<string, string>()
                {
                },
                title: "Open dialog", 
                icon: "", 
                text: "Open dialog",
                actionName: $"dialog{nameof(PartialDialog.OpenedDialogId)}",
                actionValue: "dialog");
        }

        private PartialDialog BuildDialog()
        {
            var result = new PartialDialog(
                "dialog",
                new Dictionary<string, string>()
                {
                },
                "Dialog",
                BuildDialogContent(),
                null);
            result.FillFromQueryCollection(Request.Query);

            return result;
        }

        private PartialDialogContent BuildDialogContent()
        {
            var result = new PartialDialogContent(
                "dialogcontent",
                new Dictionary<string, string>()
                {
                });
            result.FillFromQueryCollection(Request.Query);

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

            if (!string.IsNullOrEmpty(viewModel.Dialog.OpenedDialogId))
            {
                viewModel.PartialTabs.HiddenNameActions.Add(
                    new KeyValuePair<string, string>($"dialog{nameof(PartialDialog.OpenedDialogId)}",
                        viewModel.Dialog.OpenedDialogId));
            }

            if (!string.IsNullOrEmpty(viewModel.PartialTabs.SelectedTabId))
            {
                viewModel.OpenDialogButton.HiddenNameActions.Add(
                    new KeyValuePair<string, string>($"tabs{nameof(PartialTabs.SelectedTabId)}",
                        viewModel.PartialTabs.SelectedTabId));
                viewModel.Dialog.HiddenNameActions.Add(
                    new KeyValuePair<string, string>($"tabs{nameof(PartialTabs.SelectedTabId)}",
                        viewModel.PartialTabs.SelectedTabId));
            }

            return View(viewModel);
        }
    }
}
