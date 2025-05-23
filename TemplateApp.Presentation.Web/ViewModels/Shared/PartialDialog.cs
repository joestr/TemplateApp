﻿using TemplateApp.Presentation.Web.Misc;

namespace TemplateApp.Presentation.Web.ViewModels.Shared;

public class PartialDialog : WebAppComponentViewModel
{
    public string Title { get; set; } = "";
    public PartialDialogContent Content { get; set; } = new();
    public string? OpenedDialog;

    public PartialDialog()
    {
    }

    public PartialDialog(
        string identifier,
        IDictionary<string, string> hiddenInputNameAndValues,
        IQueryCollection queryCollection,
        string title,
        PartialDialogContent content,
        string? openedDialog) : base(identifier, hiddenInputNameAndValues, queryCollection)
    {
        Title = title;
        Content = content;
        OpenedDialog = openedDialog;
        FillFromQuery();
    }

    public override void FillFromQuery(IQueryCollection queryCollection)
    {
        OpenedDialog = this.GetStringFromQueryParameter(queryCollection, nameof(OpenedDialog));
    }

    public override List<KeyValuePair<string, string>> GetHiddenInputNameAndValues()
    {
        var result = new List<KeyValuePair<string, string>>();
        result = this.AddNullableToList(result, this.GetHiddenInputNameAndValue(nameof(OpenedDialog), OpenedDialog));
        return result;
    }
}