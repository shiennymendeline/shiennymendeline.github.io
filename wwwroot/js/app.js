const timestamp = new Date().getTime();
const stylesheets = document.querySelectorAll('link[rel="stylesheet"]');

stylesheets.forEach(stylesheet => {
    const currentHref = stylesheet.getAttribute('href');
    const newHref = currentHref.includes('?')
        ? `${currentHref}&v=${timestamp}`
        : `${currentHref}?v=${timestamp}`;

    stylesheet.setAttribute('href', newHref);
});
const scripts = document.querySelectorAll('script[src]');
scripts.forEach(script => {
    const currentSrc = script.getAttribute('src');
    const newSrc = currentSrc.includes('?')
        ? `${currentSrc}&v=${timestamp}`
        : `${currentSrc}?v=${timestamp}`;
    script.setAttribute('src', newSrc);
});

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