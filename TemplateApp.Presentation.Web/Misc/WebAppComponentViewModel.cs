
using Newtonsoft.Json;

namespace TemplateApp.Presentation.Web.Misc;

public abstract class WebAppComponentViewModel
{
    public string Identifier { get; set; } = "";
    public IDictionary<string, string> HiddenInputNameAndValues { get; set; } = new Dictionary<string, string>();
    public IQueryCollection QueryCollection { get; set; } = new QueryCollection();

    public WebAppComponentViewModel()
    {
    }

    public WebAppComponentViewModel(string identifier, IDictionary<string, string> hiddenInputNameAndValues, IQueryCollection queryCollection)
    {
        Identifier = identifier;
        HiddenInputNameAndValues = hiddenInputNameAndValues;
        QueryCollection = queryCollection;
    }

    protected void FillFromQuery()
    {
        FillFromQuery(QueryCollection);
    }

    public abstract void FillFromQuery(IQueryCollection queryCollection);
    
    public ulong? GetUlongFromQueryParameter(IQueryCollection queryCollection, string queryParameter)
    {
        ulong? result = null;
        var isResulValid = ulong.TryParse(queryCollection[Identifier + queryParameter].FirstOrDefault(), out var parsedResult);

        if (isResulValid)
        {
            result = parsedResult;
        }

        return result;
    }

    public DateTimeOffset? GetDateTimeOffsetFromQueryParameter(IQueryCollection queryCollection, string queryParameter)
    {
        DateTimeOffset? result = null;
        var isResulValid = DateTimeOffset.TryParse(queryCollection[Identifier + queryParameter].FirstOrDefault(), out var parsedResult);

        if (isResulValid)
        {
            result = parsedResult;
        }

        return result;
    }

    public bool? GetBooleanFromQueryParameter(IQueryCollection queryCollection, string queryParameter)
    {
        bool? result = null;
        var isResulValid = bool.TryParse(queryCollection[Identifier + queryParameter].FirstOrDefault(), out var parsedResult);

        if (isResulValid)
        {
            result = parsedResult;
        }

        return result;
    }

    public string? GetStringFromQueryParameter(IQueryCollection queryCollection, string queryParameter)
    {
        var result = queryCollection[Identifier + queryParameter].FirstOrDefault();
        return result;
    }

    public string[]? GetStringArrayFromQueryParameter(IQueryCollection queryCollection, string queryParameter)
    {
        var result = Array.Empty<string>();

        var stringArrayAsString = queryCollection[Identifier + queryParameter].FirstOrDefault();
        if (stringArrayAsString != null)
        {
            result = JsonConvert.DeserializeObject<string[]>(stringArrayAsString);
        }

        return result;
    }

    public abstract List<KeyValuePair<string, string>> GetHiddenInputNameAndValues();

    public List<KeyValuePair<string, string>> AddNullableToList(List<KeyValuePair<string, string>> list, KeyValuePair<string, string>? value)
    {
        if (value.HasValue)
        {
            list.Add(value.Value);
        }

        return list;
    }

    public KeyValuePair<string, string>? GetHiddenInputNameAndValue(string name, ulong? value)
    {
        if (value.HasValue)
        {
            return new KeyValuePair<string, string>(Identifier + name, value.Value.ToString());
        }

        return null;
    }
    
    public KeyValuePair<string, string>? GetHiddenInputNameAndValue(string name, DateTimeOffset? value)
    {
        if (value.HasValue)
        {
            return new KeyValuePair<string, string>(Identifier + name, value.Value.ToString());
        }

        return null;
    }
    
    public KeyValuePair<string, string>? GetHiddenInputNameAndValue(string name, bool? value)
    {
        if (value.HasValue)
        {
            return new KeyValuePair<string, string>(Identifier + name, value.Value.ToString());
        }

        return null;
    }
    
    public KeyValuePair<string, string>? GetHiddenInputNameAndValue(string name, string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return new KeyValuePair<string, string>(Identifier + name, value);
        }

        return null;
    }

    public KeyValuePair<string, string>? GetHiddenInputNameAndValue(string name, string[]? value)
    {
        if (value != null && value != Array.Empty<string>())
        {
            return new KeyValuePair<string, string>(Identifier + name, JsonConvert.SerializeObject(value));
        }

        return null;
    }
}