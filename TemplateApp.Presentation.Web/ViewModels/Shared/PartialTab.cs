namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialTab
{
    public string View { get; set; }
    public object ViewModel { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }

    public PartialTab(string view, object viewModel, string id, string title)
    {
        View = view;
        ViewModel = viewModel;
        Id = id;
        Title = title;
    }
}