
using Microsoft.JSInterop;

namespace shiennymendeline.github.io.Services.Implements
{
    public class JSService(IJSRuntime JSRuntime) : IJSService
    {
        public async Task InitializeListeners()
        {
            await JSRuntime.InvokeVoidAsync("InitializeListeners");
        }

        public async Task SetCurrentSectionId(string sectionId)
        {
            await JSRuntime.InvokeVoidAsync("SetCurrentSectionId", sectionId);
        }
        public async Task ScrollToSection(string sectionId)
        {
            await JSRuntime.InvokeVoidAsync("ScrollToSection", sectionId);
        }
        public async Task SetDotnetReference<Page>(DotNetObjectReference<Page> dotNetRef) where Page : class
        {
            await JSRuntime.InvokeVoidAsync("SetDotnetReference", dotNetRef);
        }
    }
}
