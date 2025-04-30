using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialDialog : WebAppComponentViewModel
{
    public string Title { get; set; } = "";
    public PartialDialogContent Content { get; set; } = new();
    public string? OpenedDialogId;

    public PartialDialog()
    {
    }

    public PartialDialog(
        string identifier,
        IDictionary<string, string> hiddenNameActions, 
        string title,
        PartialDialogContent content,
        string? openedDialogId) : base(identifier, hiddenNameActions)
    {
        Title = title;
        Content = content;
        OpenedDialogId = openedDialogId;
    }

    public void FillFromQueryCollection(IQueryCollection queryCollection)
    {
        var isOpenedDialogIdPresent
            = queryCollection[Identifier + nameof(OpenedDialogId)].FirstOrDefault();

        if (isOpenedDialogIdPresent != null)
        {
            OpenedDialogId = isOpenedDialogIdPresent;
        }
    }
}