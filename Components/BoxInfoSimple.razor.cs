using Microsoft.AspNetCore.Components;

namespace shiennymendeline.github.io.Components
{
    public partial class BoxInfoSimple : ComponentBase
    {
        [Parameter] public string Name { get; set; }
        [Parameter] public string Caption { get; set; }
        [Parameter] public string ImgPath { get; set; }
    }
}