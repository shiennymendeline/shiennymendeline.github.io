var currentSectionId = "";
const timestamp = new Date().getTime();
const stylesheets = document.querySelectorAll('link[rel="stylesheet"]');
var GLOBAL = {};
GLOBAL.DotNetReference = null;

function SetDotnetReference(pDotNetReference) {
    GLOBAL.DotNetReference = pDotNetReference;
}

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
window.ScrollToSection = function (sectionId) {
    var element = document.getElementById(sectionId);
    if (element) {
        element.scrollIntoView({ behavior: "smooth" });
    }
};

function SetCurrentSectionId(sectionId) {
    currentSectionId = sectionId;
}

function NavigateToSection(sectionId) {
    if (GLOBAL.DotNetReference !== null) {
        GLOBAL.DotNetReference.invokeMethodAsync('NavigateToSectionFromJS', sectionId);
    } else {
        console.error('DotNetReference is null. Please set it first.');
    }
}

function InitializeListeners() {
    $('section').on('mousemove', handleMouseSection);
    $('section').on('mouseenter', handleMouseSection);

    function handleMouseSection() {
        var sectionId = $(this).attr('id');
        if (currentSectionId != sectionId) {
            //console.log('Mouse is inside the section');
            window.history.replaceState(null, null, "#" + sectionId);
            SetCurrentSectionId(sectionId);
            NavigateToSection(sectionId);
        }
    }
}