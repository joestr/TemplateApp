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
        private const string TableIdentifier = "Table";
        private const string TreeIdentifier = "Tree";
        
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

        private PartialTable BuildTable()
        {
            var result = new PartialTable(
                identifier: TableIdentifier,
                new Dictionary<string, string>() {},
                queryCollection: Request.Query,
                new List<List<string>>()
                {
                    new List<string>() { "Row 1 Col 1", "Row 1 Col 2" },
                    new List<string>() { "Row 2 Col 1", "Row 2 Col 2" },
                    new List<string>() { "Row 3 Col 1", "Row 3 Col 2" },
                },
                null,
                null);

            if (result.SearchTerm != null)
            {
                var res = new List<List<string>>()
                {
                    new List<string>() { "Row 1 Col 1", "Row 1 Col 2" },
                    new List<string>() { "Row 2 Col 1", "Row 2 Col 2" },
                    new List<string>() { "Row 3 Col 1", "Row 3 Col 2" },
                };
                
               res = res.FindAll(rows => rows.Any(cols => cols.Contains(result.SearchTerm)));

               result.Grid = res;
            }

            return result;
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


        private PartialTree BuildTree()
        {
            var result = new PartialTree(
                identifier: TreeIdentifier,
                hiddenInputNameAndValues: new Dictionary<string, string>() { },
                queryCollection: Request.Query,
                treeEntries: new List<PartialTreeEntry>
                {
                    new PartialTreeEntry("Root", "root", null),
                    new PartialTreeEntry("Sub 1", "sub1", "root"),
                    new PartialTreeEntry("Sub 1.1", "sub1.1", "sub1"),
                    new PartialTreeEntry("Sub 1.1.1", "sub1.1.1", "sub1.1"),
                    new PartialTreeEntry("Sub 2", "sub2", "root")
                },
                null);

            return result;
        }

        //[Authorize]
        public IActionResult Start()
        {
            var viewModel = new Start();

            viewModel.Table = BuildTable();
            viewModel.PartialTabs = BuildTabs();
            viewModel.LinkButtonWithText = BuildLinkButtonWithText();
            viewModel.OpenDialogButton = BuildOpenDialogButton();
            viewModel.Dialog = BuildDialog();
            viewModel.Tree = BuildTree();

            viewModel.Dialog.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.PartialTabs.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.OpenDialogButton.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Table.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Tree.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });

            viewModel.PartialTabs.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.Dialog.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.OpenDialogButton.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Table.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Tree.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });
            
            viewModel.OpenDialogButton.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.Dialog.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.PartialTabs.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Table.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Tree.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });
            
            viewModel.Table.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.Dialog.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.PartialTabs.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.OpenDialogButton.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Tree.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });

            viewModel.Tree.GetHiddenInputNameAndValues().ForEach(nv =>
            {
                viewModel.Dialog.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.PartialTabs.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.OpenDialogButton.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
                viewModel.Table.HiddenInputNameAndValues.Add(nv.Key, nv.Value);
            });

            return View(viewModel);
        }
    }
}
