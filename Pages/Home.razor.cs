using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using shiennymendeline.github.io.Models;
using shiennymendeline.github.io.Services;
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
        
        public string searchSkill { get; set; } = "";
        public List<string> selectedSkillsForProject { get; set; } = new List<string>();
        public List<CardInfoItem> projects = new List<CardInfoItem>();
        public List<SkillItem> SkillItems = new List<SkillItem>();
        private bool _processing = false;



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

        private void SetupSkillItems(string searchText = "")
        {
            if (string.IsNullOrEmpty(searchText))
            {
                SkillItems = MyProfile.Skill.Items;
            }
            else
            {
                SkillItems = MyProfile.Skill.Items.Where(x => x.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
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

        public void SearchSkillsByKeyword()
        {
            _processing = true;
            SetupSkillItems(searchSkill);
            _processing = false;
            StateHasChanged();
        }

        public void ClearSearchSkill()
        {
            searchSkill = "";
            SetupSkillItems();
        }

        public void OnKeyUpSearchSkill(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                SearchSkillsByKeyword();
            }
        }

        //public Task OnValueChangedCheckboxSkillCat(string category)
        //{
        //    if (category == "all")
        //    {
        //        SetupSkillItems();
        //    }
        //    else
        //    {
        //        SetupSkillItems().Where(x => x.Category == category).ToList();
        //    }
        //}
        private void OnCategorySelected(string categoryId, bool isSelected)
        {
            Console.WriteLine($"Category ID {categoryId} selected: {isSelected}");
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
            }
            await JSService.InitializeListeners();
        }
    }
}