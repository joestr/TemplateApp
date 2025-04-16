using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialButton
{
    public string Title { get; set; }
    public string? Icon { get; set; } = null;
    public string? Text { get; set; } = null;
    public WebAppJavaScriptFunctionCalls OnClick { get; set; }

    public PartialButton(string title, string? icon, string? text, WebAppJavaScriptFunctionCalls onClick)
    {
        Title = title;
        Icon = icon;
        Text = text;
        OnClick = onClick;
    }
}