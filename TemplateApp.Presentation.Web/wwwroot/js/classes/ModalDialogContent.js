class ModalDialogContent {

    identifier = "";
    refreshUrl = "";
    refreshOnEvents = [];
    contentElementId = "";

    constructor(identifier, refreshUrl, refreshOnEvents, contentElementId) {
        this.identifier = identifier;
        this.refreshUrl = refreshUrl;
        this.refreshOnEvents = refreshOnEvents;
        this.contentElementId = contentElementId;

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
    }

    beforeContentReplace() {
    }

    afterContentReplace() {
    }

    onOpenedModalDialog(event) {
        this.refresh();
    }
}