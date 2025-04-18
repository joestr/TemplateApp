﻿using TemplateApp.Presentation.Web.ViewModels.Shared;

namespace TemplateApp.ViewModels
{
    public class Start
    {
        public PartialRefreshTable? FuelPriceTable { get; set; }
        public PartialTabs PartialTabs { get; set; }
        public PartialLinkButton LinkButtonWithText { get; set; }
        public PartialButton OpenDialogButton { get; set; }
        public PartialModalDialog Dialog { get; set; }
    }
}
