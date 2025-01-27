using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using shiennymendeline.github.io.Components;
using shiennymendeline.github.io.Models;
using shiennymendeline.github.io.Services;
using System.Net.Http.Json;
using static System.Collections.Specialized.BitVector32;

namespace shiennymendeline.github.io.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] IJSService JSService { get; set; } = default!;
        [Inject] NavigationManager _NavigationManager { get; set; } = default!;
        [Inject] HttpClient Http { get; set; } = default!;
        [Inject] ISnackbar Snackbar { get; set; } = default!;
        public MyProfile MyProfile { get; set; } = default!;
        InputGroupButton IgbSearchSkill = new();
        
        public List<SkillItem> SkillItems = new List<SkillItem>();
        public List<CardInfoItem> projects = new List<CardInfoItem>();

        private string currentSectionId = "";
        private DotNetObjectReference<Home> _dotNetRef = default!;

        private IEnumerable<string> options { get; set; } = new HashSet<string>();

        protected async override Task OnInitializedAsync()
        {
            try
            {
                MyProfile = await Http.GetFromJsonAsync<MyProfile>($"data/profileinfo.json?{Guid.NewGuid()}");
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
                    SetupProjects();
                    await JSService.InitializeListeners();
                }
            }
            catch
            {
                Snackbar.Add("Failed to load profile data", Severity.Error);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetRef = DotNetObjectReference.Create(this);
                await JSService.SetDotnetReference(_dotNetRef);
            }
            await NavigateToSectionByUrl();
        }

        private void SetupProjects()
        {
            options = MyProfile.Skill.Items.Select(x => x.Name);
            SearchProjects(options);
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

        private void SearchProjects(IEnumerable<string> selectedOptions)
        {
            options = selectedOptions;
            projects = MyProfile.Project.Items.Where(x => x.Tags.Intersect(options).Any()).ToList();
            projects.ForEach(x => x.Tags = x.Tags.Intersect(options).ToArray());
            StateHasChanged();
        }
        private string GetSelectedProjectSkillText(IEnumerable<string> selectedItems)
        {
            var count = selectedItems.Count();
            if (count == 0 || count == MyProfile.Skill.Items.Count)
            {
                return MyProfile.Project.SelecAllText;
            }
            else if (count == 1)
            {
                return selectedItems.First();
            }
            else
            {
                return $"{count} skills selected";
            }

        }

        private async Task NavigateToSectionByUrl()
        {
            var currentUrl = _NavigationManager.Uri.Split('#');
            if (currentUrl.Length > 1 && currentSectionId == "")
            {
                var sectionId = "#" + currentUrl[1];
                _NavigationManager.NavigateTo(sectionId);
                if (_dotNetRef != null)
                {
                    await JSService.SetCurrentSectionId(sectionId);
                }
            }
        }
        
        [JSInvokable]
        public void NavigateToSectionFromJS(string sectionId)
        {
            currentSectionId = sectionId;
        }
    }
}