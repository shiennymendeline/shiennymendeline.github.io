using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using shiennymendeline.github.io.Models;
using shiennymendeline.github.io.Services;

namespace shiennymendeline.github.io.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] IJSService JSService { get; set; } = default!;

        public string searchSkill { get; set; } = "";
        public List<string> selectedSkillsForProject { get; set; } = new List<string>();
        public List<ItemOption> skillCategories { get; set; } = new()
        {
            new("all", "ALL", true),
            new("fe", "FRONT END"),
            new("be", "BACK END"),
            new("db", "DATABASE"),
            new("vc", "VERSION CONTROL"),
            new("ss", "SOFT SKILLS")
        };
        public string VerifiedImgPath { get; set; } = "images/verify.png";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSService.InitializeListeners();
            }
        }
    }
}