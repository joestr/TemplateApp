using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialButton : WebAppComponentViewModel
{
    public string Title { get; set; }
    public string? Icon { get; set; } = null;
    public string? Text { get; set; } = null;
    
    public string? ActionName { get; set; } = null;
    public string? ActionValue { get; set; }

    public PartialButton(
        string identifier,
        IDictionary<string, string> hiddenNameAndNamesAnd,
        IQueryCollection queryCollection,
        string title, string? icon, string? text, string? actionName, string? actionValue) : base(identifier, hiddenNameAndNamesAnd, queryCollection)
    {
        Title = title;
        Icon = icon;
        Text = text;
        ActionName = actionName;
        ActionValue = actionValue;
        FillFromQuery();
    }

    public override void FillFromQuery(IQueryCollection queryCollection)
    {
    }

    public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
    {
        var result = new List<KeyValuePair<string, string>>();
        return result;
    }
}