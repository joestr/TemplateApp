class Tabs {

    identifier = "";
    refreshUrl = "";
    refreshOnEvents = [];
    contentElementId = "";
    selectedTabId = null;
    /* Tabs created by Materialize */
    materializeTabs = [];

    constructor(identifier, refreshUrl, refreshOnEvents, contentElementId, selectedTabId) {
        this.identifier = identifier;
        this.refreshUrl = refreshUrl;
        this.refreshOnEvents = refreshOnEvents;
        this.contentElementId = contentElementId;
        this.selectedTabId = selectedTabId;

        this.init();
        this.afterInit();
    }

    init() {
        for (let i = 0; i < this.refreshOnEvents.length; i++) {
            document.addEventListener(this.refreshOnEvents[i].Name, (event) => this[this.refreshOnEvents[i].Function](event));
        }
    }

    refresh() {
        let refreshContentElement = document.getElementById(this.contentElementId);

        let url = new URL(this.refreshUrl, window.location.origin);

        /* Get a possible selected tab id */
        if (this.selectedTabId != null) {
            url.searchParams.append(this.identifier + "_selectedTabId", this.selectedTabId);

            let windowUrl = new URL(window.location);
            windowUrl.searchParams.set(this.identifier + "_selectedTabId", this.selectedTabId);
            history.pushState(null, '', windowUrl);
        } else {
            url.searchParams.delete(this.identifier + "_selectedTabId");

            let windowUrl = new URL(window.location);
            windowUrl.searchParams.delete(this.identifier + "_selectedTabId");
            history.pushState(null, '', windowUrl);
        }

        fetch(url).then(
            (response) => {
                if (!response.ok) {
                    return;
                }

                this.beforeContentReplace();

                /* Convert response to text (for HTML) */
                response.text().then((string) => {
                    /* Create new template element and set the response content it */
                    let template = document.createElement("template");
                    template.innerHTML = string;

                    /* Replace the content with the new one */
                    let templateContent = template.content.getElementById(this.contentElementId);
                    refreshContentElement.replaceWith(templateContent);

                    this.afterContentReplace();
                });
            },
            (rejected) => {
            }
        );
    }

    afterInit() {
        this.createMaterializeTabs();
    }

    beforeContentReplace() {
        this.destroyMaterializeTabs();
    }

    afterContentReplace() {
        this.createMaterializeTabs();
    }

    destroyMaterializeTabs() {
        this.materializeTabs[0].destroy();
    }

    createMaterializeTabs() {
        let elems = document.getElementById(this.contentElementId).getElementsByClassName("tabs");
        this.materializeTabs = M.Tabs.init(elems, {
        });

        if (this.materializeTabs == null) {
            this.materializeTabs = [];
        } else {
            setTimeout((handler) => {
                this.materializeTabs[0].updateTabIndicator();
            }, this.materializeTabs[0].options.duration);
        }
    }

    /**
     * The ID of the tab to select and highlight.
     * @param {Number} selectedTabId The ID of the tab.
     * @param {Event} event An optional event.
     */
    selectTabId(selectedTabId, event) {
        this.selectedTabId = selectedTabId;
        
        if (selectedTabId == null) {
            /* If the selected tab ID is null it's enough to only refresh
            * the content from the server side. */
            this.refresh();
        } else {
            this.materializeTabs[0].select(`${this.identifier}_${this.selectedTabId}`);
            this.materializeTabs[0].updateTabIndicator();
            setTimeout((handler) => {
                this.refresh();
            }, this.materializeTabs[0].options.duration);
        }

        if (event != null) {
            event.preventDefault();
            /* event.stopPropagation(); */
        }
    }

    //#region Custom event handlers
    //#endregion
}