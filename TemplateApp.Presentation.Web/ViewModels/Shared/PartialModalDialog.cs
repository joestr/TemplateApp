using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialModalDialog : WebAppComponentViewModel<PartialModalDialog>
{
    public static Func<string> ClassName = () =>
    {
        var classNameString = nameof(PartialModalDialog);
        var partialLength = "Partial".Length;
        return classNameString.Substring(partialLength, classNameString.Length - partialLength);
    };

    public static Func<string> MethodNameOpenModalDialog =
        () => "openModalDialog";
    public static Func<string?, string[]> MethodArgsOpenModalDialog =
        (modalDialogId) => new string[]
        {
            modalDialogId != null ? "\"" + modalDialogId + "\"" : "null",
            "event"
        };

    public static Func<string> EventTriggerNameOpenedModalDialog =
        () => "openedModalDialog";

    public string Title { get; set; } = "";
    public PartialModalDialogContent Content { get; set; } = new();
    public WebAppJavaScriptFunctionCalls OnOkClick { get; set; } = new();
    public string OnOkClickFunctionCalls => OnOkClick.FunctionCalls;
    public string? OpenedModalDialogId;

    public PartialModalDialog()
    {
    }

    public PartialModalDialog(
        string identifier,
        string refreshUrl,
        List<WebAppRefreshOnEvent> refreshOnEvents,
        string title,
        PartialModalDialogContent content,
        WebAppJavaScriptFunctionCalls onOkClick,
        string? openedModalDialogId) : base(identifier, refreshUrl, refreshOnEvents)
    {
        Title = title;
        Content = content;
        OnOkClick = onOkClick;
        OpenedModalDialogId = openedModalDialogId;
    }

    public void FillFromQueryCollection(IQueryCollection queryCollection)
    {
        var isOpenedModalDialogIdPresent
            = queryCollection[Identifier + "_openedModalDialogId"].FirstOrDefault();

        if (isOpenedModalDialogIdPresent != null)
        {
            OpenedModalDialogId = isOpenedModalDialogIdPresent;
        }
    }
}