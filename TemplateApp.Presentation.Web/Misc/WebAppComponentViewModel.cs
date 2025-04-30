
using Newtonsoft.Json;

namespace TemplateApp.Presentation.Web.Misc;

public class WebAppComponentViewModel
{
    public string Identifier { get; set; } = "";
    public IDictionary<string, string> HiddenNameActions { get; set; } = new Dictionary<string, string>();

    public WebAppComponentViewModel()
    {
    }

    public WebAppComponentViewModel(string identifier, IDictionary<string, string> hiddenNameActions)
    {
        Identifier = identifier;
        HiddenNameActions = hiddenNameActions;
    }
}