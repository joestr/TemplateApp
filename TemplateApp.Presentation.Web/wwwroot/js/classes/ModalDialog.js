class ModalDialog {

    identifier = "";
    refreshUrl = "";
    refreshOnEvents = [];
    contentElementId = "";
    openedModalDialogId = null;
    materializeModals = [];

    constructor(identifier, refreshUrl, refreshOnEvents, contentElementId, openedModalDialogId) {
        this.identifier = identifier;
        this.refreshUrl = refreshUrl;
        this.refreshOnEvents = refreshOnEvents;
        this.contentElementId = contentElementId;
        this.openedModalDialogId = openedModalDialogId;

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

        /* Get a possible opened dialog ID */
        if (this.openedModalDialogId != null) {
            url.searchParams.append(this.identifier + "_openedModalDialogId", this.openedModalDialogId);

            let windowUrl = new URL(window.location);
            windowUrl.searchParams.set(this.identifier + "_openedModalDialogId", this.openedModalDialogId);
            history.pushState(null, '', windowUrl);
        } else {
            url.searchParams.delete(this.identifier + "_openedModalDialogId");

            let windowUrl = new URL(window.location);
            windowUrl.searchParams.delete(this.identifier + "_openedModalDialogId");
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
        this.createMaterializeModals();
    }

    beforeContentReplace() {
        this.destroyMaterializeModals();
    }

    afterContentReplace() {
        this.createMaterializeModals();
    }

    /**
     * Display the dialog for the given ID.
     * @param {Number} dialogId The ID of the Bill-of-Material position.
     * @param {Event} event The passed event.
     */
    openModalDialog(modalDialogId, event = null) {
        this.openedModalDialogId = modalDialogId;
        this.refresh();

        if (event != null) {
            event.preventDefault();
            event.stopPropagation();
        }
    }

    destroyMaterializeModals() {
        this.materializeModals.forEach(materializeModal => {
            materializeModal.destroy();
        });
    }

    createMaterializeModals() {
        let elem = document.getElementById(this.contentElementId);
        let elems = null;

        if (elem == null) {
            elems == null;
        } else {
            elems = elem.getElementsByClassName("modal");
        }

        if (elems == null) {
            this.materializeModals = [];
            return;
        }

        this.materializeModals = M.Modal.init(elems, {
            onCloseEnd: () => {
                this.openModalDialog(null, null);
            }
        });

        if (this.materializeModals == null) {
            this.materializeModals = [];
        }

        for (let idx = 0; idx < this.materializeModals.length; idx++) {
            let materializeModal = this.materializeModals[idx];

            let modalDialogId = materializeModal.el.getAttribute("data-id");

            if (this.openedModalDialogId != null && this.openedModalDialogId === modalDialogId) {
                materializeModal.open();
            }

            var event = new CustomEvent(
                `${ModalDialog.name}_${this.identifier}_openedModalDialog`,
                { detail: { dialogId: this.openedModalDialogId } });
            document.dispatchEvent(event);
        }
    }
}