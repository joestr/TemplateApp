using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialTable : WebAppComponentViewModel
{
    public string Title { get; set; } = "";
    public PartialDialogContent Content { get; set; } = new();

    public PartialTable()
    {
    }

    public PartialTable(
        string identifier,
        IDictionary<string, string> hiddenInputNameAndValues,
        IQueryCollection queryCollection,
        string? openedDialog) : base(identifier, hiddenInputNameAndValues, queryCollection)
    {
        FillFromQuery();
    }

    public override void FillFromQuery(IQueryCollection queryCollection)
    {
    }

    public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
    {
        return new List<KeyValuePair<string, string>>();
    }
}