namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialTreeEntry
{
    public string Text { get; set; }
    public string Id { get; set; }
    public string? ParentId { get; set; }

    public PartialTreeEntry(string text, string id, string? parentId)
    {
        Text = text;
        Id = id;
        ParentId = parentId;
    }
}