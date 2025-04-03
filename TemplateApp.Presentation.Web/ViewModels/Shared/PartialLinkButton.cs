namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialLinkButton
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string? Icon { get; set; } = null;
    public string? Text { get; set; } = null;

    public PartialLinkButton(string title, string url, string? icon, string? text)
    {
        Title = title;
        Url = url;
        Icon = icon;
        Text = text;
    }
}