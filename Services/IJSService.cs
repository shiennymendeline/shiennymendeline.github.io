using Microsoft.JSInterop;

namespace shiennymendeline.github.io.Services
{
    public interface IJSService
    {
        Task InitializeListeners();
        Task SetCurrentSectionId(string sectionId);
        Task ScrollToSection(string sectionId);
        Task SetDotnetReference<Page>(DotNetObjectReference<Page> dotNetRef) where Page : class;
    }
}