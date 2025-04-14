using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialLinkButton
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string? Icon { get; set; } = null;
    public string? Text { get; set; } = null;
    public AnchorTarget Target { get; set; }

    public PartialLinkButton(string title, string url, string? icon, string? text, AnchorTarget target)
    {
        Title = title;
        Url = url;
        Icon = icon;
        Text = text;
        Target = target;
    }

    public string ResolveTarget()
    {
        var result = "";
        switch (Target)
        {
            case AnchorTarget.Top: result = "_top"; break;
            case AnchorTarget.Blank: result = "_blank"; break;
            case AnchorTarget.Parent: result = "_parent"; break;
            case AnchorTarget.Self: result = "_self"; break;
        }
        return result;
    }
}