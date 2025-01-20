using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace shiennymendeline.github.io.Components
{
    public partial class InputGroupButton : ComponentBase
    {
        private bool _processing = false;
        [Parameter] public string Placeholder { get; set; } = "";
        [Parameter] public string ButtonText { get; set; } = "";
        [Parameter] public Action OnClickButtonEvent { get; set; }
        public string SearchText { get; set; } = "";
        public void StartProcessing()
        {
            _processing = true;
        }
        public void StopProcessing()
        {
            _processing = false;
        }
        public void ClearSearch()
        {
            SearchText = "";
            CallActionClick();
        }
        public void OnKeyUpSearch(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                CallActionClick();
            }
        }

        public void CallActionClick()
        {
            StartProcessing();
            OnClickButtonEvent();
            StopProcessing();
        }
    }
}