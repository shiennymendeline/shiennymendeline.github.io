using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using shiennymendeline.github.io.Components;
using shiennymendeline.github.io.Models;
using shiennymendeline.github.io.Services;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using static shiennymendeline.github.io.Pages.Weather;
using static System.Net.WebRequestMethods;

namespace shiennymendeline.github.io.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] IJSService JSService { get; set; } = default!;
        [Inject] HttpClient Http { get; set; } = default!;
        [Inject] ISnackbar Snackbar { get; set; } = default!;
        public MyProfile MyProfile { get; set; } = default!;
        
        //public string searchSkill { get; set; } = "";
        public List<string> selectedSkillsForProject { get; set; } = new List<string>();
        public List<CardInfoItem> projects = new List<CardInfoItem>();
        public List<SkillItem> SkillItems = new List<SkillItem>();

        InputGroupButton IgbSearchSkill = new();


        private string value { get; set; } = "Nothing selected";
        private IEnumerable<string> options { get; set; } = new HashSet<string>() { "Lion" };

        private string[] felines =
        {
        "Jaguar", "Leopard", "Lion", "Lynx", "Panther", "Puma", "Tiger"
    };

        private string GetMultiSelectionText(List<string> selectedValues)
        {
            return $"{selectedValues.Count} feline{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                MyProfile = await Http.GetFromJsonAsync<MyProfile>("data/profileinfo.json");
                if (MyProfile == null)
                {
                    Snackbar.Add("Failed to load profile data", Severity.Error);
                    return;
                }
                else
                {
                    SetupSkillCategories();
                    SetupSkillItems();
                    SetupProfileInfo();
                    await JSService.InitializeListeners();
                }
            }
            catch
            {
                Snackbar.Add("Failed to load profile data", Severity.Error);
            }
        }

        private void SetupSkillItems()
        {
            var arr = MyProfile.Skill.Categories.Where(y => y.IsSelected).Select(y => y.Id);

            SkillItems = MyProfile.Skill.Items
                                  .Where(x => x.Name.Contains(IgbSearchSkill.SearchText, StringComparison.OrdinalIgnoreCase))
                                  .Where(x => arr.Contains(x.Category) || arr.Contains("all"))
                                  .ToList();
            StateHasChanged();
        }

        private void SetupProfileInfo()
        {
            var tmpStr = MyProfile.Profile.Summary.Replace("[", "<span>").Replace("]","</span>");
            MyProfile.Profile.Summary = tmpStr;
        }

        private void SetupSkillCategories()
        {
            MyProfile.Skill.Categories.Insert(0, new ItemOption("all", MyProfile.Skill.AllInfo, true));
        }

        private void OnCheckboxChanged(string id, bool value)
        {
            if (id == "all")
            {
                MyProfile.Skill.Categories.ForEach(x => x.IsSelected = !value);
                MyProfile.Skill.Categories[0].IsSelected = value;
            }
            else
            {
                MyProfile.Skill.Categories.Where(x => x.Id == id).First().IsSelected = value;
                MyProfile.Skill.Categories[0].IsSelected = false;
            }
            SetupSkillItems();
        }

    }
}