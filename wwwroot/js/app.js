function InitializeListeners() {
    $('.chip-info-cb').on("change", function () {
        var parentElement = this.closest('.chip-info')
        if (this.ariaLabel == "all") {
            $('.chip-info').removeClass("chip-info-checked")
            $('.chip-info input[type="checkbox"]:not([aria-label="all"])').prop('checked', false)
        }
        else {
            $('#chipall').removeClass("chip-info-checked")
            $('#chipall input[type="checkbox"]').prop('checked', false)
        }
        if (this.checked) {
            $(parentElement).addClass("chip-info-checked")
        } else {
            $(parentElement).removeClass("chip-info-checked")
        }
    })
}