using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialTable : WebAppComponentViewModel
{
    public List<List<string>> Grid { get; set; } = new List<List<string>>();
    public string? SearchTerm { get; set; } = "";

    public PartialTable()
    {
    }

    public PartialTable(
        string identifier,
        IDictionary<string, string> hiddenInputNameAndValues,
        IQueryCollection queryCollection,
        List<List<string>> grid,
        bool? refresh,
        string? searchTerm) : base(identifier, hiddenInputNameAndValues, queryCollection)
    {
        Grid = grid;
        SearchTerm = searchTerm;
        FillFromQuery();
    }

    public override void FillFromQuery(IQueryCollection queryCollection)
    {
        SearchTerm = GetStringFromQueryParameter(queryCollection, nameof(SearchTerm));
    }

    public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
    {
        var result = new List<KeyValuePair<string, string>>();
        result = this.AddNullableToList(result, this.GetHiddenInputNameAndValue(nameof(SearchTerm), SearchTerm));
        return result;
    }
}