
using Microsoft.JSInterop;

namespace shiennymendeline.github.io.Services.Implements
{
    public class JSService(IJSRuntime JSRuntime) : IJSService
    {
        public async Task InitializeListeners()
        {
            await JSRuntime.InvokeVoidAsync("InitializeListeners");
        }
    }
}
