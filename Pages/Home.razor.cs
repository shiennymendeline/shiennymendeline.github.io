using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using shiennymendeline.github.io.Models;
using shiennymendeline.github.io.Services;
using System.Net.Http.Json;
using static shiennymendeline.github.io.Pages.Weather;
using static System.Net.WebRequestMethods;

namespace shiennymendeline.github.io.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] IJSService JSService { get; set; } = default!;
        [Inject] HttpClient Http { get; set; } = default!;

        
        public MyProfile myProfile { get; set; } = new MyProfile();

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
        public List<CardInfoItem> projects = new List<CardInfoItem>()
        {
            new CardInfoItem()
            {
                ImgPath = "images/projects/project_pabrikgula.jpg",
                Title = "Langlang Buana",
                Caption = "A Platform for Travelers to Share Global Travel Insights",
                ButtonPrimaryName = "GITHUB",
                ButtonPrimaryLink = "#",
                ButtonSecondaryName = "VIDEO",
                ButtonSecondaryLink = "#"
            }
        };

        protected async override Task OnInitializedAsync()
        {
            myProfile = await Http.GetFromJsonAsync<MyProfile>("data/profileinfo.json");
            if (myProfile == null)
            {
                
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSService.InitializeListeners();
            }
        }
    }
}