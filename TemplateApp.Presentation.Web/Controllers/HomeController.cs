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
        private static string FuelPriceRefreshTableIdentifier = "FuelPriceTable";

        private readonly ILogger<HomeController> _logger;
        private readonly List<IDictionary<string, object>> _tableData;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            _tableData = new List<IDictionary<string, object>>();
            var row = new Dictionary<string, object>();
            row.Add("ID", 3);
            row.Add("Name", "Super");
            row.Add("Price", 1.429M);
            _tableData.Add(row);
            var row2 = new Dictionary<string, object>();
            row2.Add("ID", 4);
            row2.Add("Name", "Diesel");
            row2.Add("Price", 1.401M);
            _tableData.Add(row2);
            var row3 = new Dictionary<string, object>();
            row3.Add("ID", 5);
            row3.Add("Name", "Racing Diesel");
            row3.Add("Price", 1.594M);
            _tableData.Add(row3);
            var row4 = new Dictionary<string, object>();
            row4.Add("ID", 6);
            row4.Add("Name", "Super Plus");
            row4.Add("Price", 1.610M);
            _tableData.Add(row4);
            var row5 = new Dictionary<string, object>();
            row5.Add("ID", 7);
            row5.Add("Name", "Super Plus Plus");
            row5.Add("Price", 1.710M);
            _tableData.Add(row5);
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return new RedirectToActionResult("Start", "Home", null);
            }

            return View();
        }

        private PartialRefreshTable BuildFuelPriceTable()
        {
            var allData = _tableData;

            var result = new PartialRefreshTable(
                FuelPriceRefreshTableIdentifier,
                allData,
                allData.Count,
                allData.Count,
                Url.Action("FuelPriceTable", "Home"));
            result.FillFromQueryCollection(Request.Query);

            var filteredData = allData;
            if (!string.IsNullOrEmpty(result.SearchTerm))
            {
                filteredData = filteredData
                    .Where(entry => entry.Values.Any(value => value.ToString().Contains(result.SearchTerm)))
                    .ToList();
                result.Count = filteredData.Count;
            }

            var pagedData = filteredData
                .Skip(result.PerPageItemCount * (result.Page - 1))
                .Take(result.PerPageItemCount)
                .ToList();
            result.Entries = pagedData;

            return result;
        }

        private PartialTabs BuildTabs()
        {
            var result = new PartialTabs(
                "tabs",
                Url.Action("Tabs", "Home") ?? "",
                [],
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

        private PartialLinkButton BuildOpenDialogButton()
        {
            return new PartialLinkButton(
                "Button with text", 
                new WebAppJavaScriptFunctionCall(
                    PartialModalDialog.ClassName(),
                    "dialog",
                    PartialModalDialog.MethodNameOpenModalDialog(),
                    PartialModalDialog.MethodArgsOpenModalDialog("dialog1")).FunctionCall, 
                "", 
                "Open dialog",
                    AnchorTarget.Self);
        }

        private PartialModalDialog BuildDialog()
        {
            var result = new PartialModalDialog(
                "dialog",
                Url.Action("Dialog", "Home") ?? "",
                [],
                "Dialog",
                BuildDialogContent(),
                new WebAppJavaScriptFunctionCalls(
                    new WebAppJavaScriptFunctionCall(
                        PartialModalDialog.ClassName(),
                        "dialog",
                        PartialModalDialog.MethodNameOpenModalDialog(),
                        PartialModalDialog.MethodArgsOpenModalDialog(null))),
                null);
            result.FillFromQueryCollection(Request.Query);

            return result;
        }

        private PartialModalDialogContent BuildDialogContent()
        {
            var result = new PartialModalDialogContent(
                "dialogcontent",
                Url.Action("DialogContent", "Home") ?? "",
                [
                    new WebAppRefreshOnEvent(
                        PartialModalDialog.ClassName(),
                        "dialog",
                        PartialModalDialog.EventTriggerNameOpenedModalDialog(),
                        PartialModalDialogContent.EventHandlerNameOnOpenedModalDialog())]);
            result.FillFromQueryCollection(Request.Query);

            return result;
        }

        //[Authorize]
        public IActionResult Start()
        {
            var viewModel = new Start();

            viewModel.FuelPriceTable = BuildFuelPriceTable();
            viewModel.PartialTabs = BuildTabs();
            viewModel.LinkButtonWithText = BuildLinkButtonWithText();
            viewModel.OpenDialogButton = BuildOpenDialogButton();
            viewModel.Dialog = BuildDialog();

            return View(viewModel);
        }

        public IActionResult FuelPriceTable()
        {
            var viewModel = BuildFuelPriceTable();
            return View("_PartialRefreshTable", viewModel);
        }

        public IActionResult Tabs()
        {
            var viewModel = BuildTabs();
            return View("_PartialTabs", viewModel);
        }

        public IActionResult Dialog()
        {
            var viewModel = BuildDialog();
            return View("_PartialModalDialog", viewModel);
        }

        public IActionResult DialogContent()
        {
            var viewModel = BuildDialogContent();
            viewModel.IncludeScript = false;
            viewModel.IncludeContent = true;
            return View("_PartialModalDialogContent", viewModel);
        }
    }
}
