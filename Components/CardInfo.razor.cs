using Microsoft.AspNetCore.Components;
using shiennymendeline.github.io.Models;

namespace shiennymendeline.github.io.Components
{
    public partial class CardInfo : ComponentBase
    {
        [Parameter]
        public CardInfoItem payload { get; set; } = new();
        [Parameter]
        public IEnumerable<string> HighlightTags { get; set; } = new List<string>();
        [Parameter]
        public IEnumerable<string> ActiveTags { get; set; } = new List<string>();
    }
}