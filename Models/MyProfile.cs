namespace shiennymendeline.github.io.Models
{
    public class MyProfile
    {
        public Profile Profile { get; set; }
        public Aboutme Aboutme { get; set; }
        public Skill Skill { get; set; }
        public Project Project { get; set; }
        public Contactme Contactme { get; set; }
        public string Footer { get; set; }
    }

    public class Profile
    {
        public string Greeting1 { get; set; }
        public string Greeting2 { get; set; }
        public string Summary { get; set; }
        public string ProfileImg { get; set; }
        public string GotoLink { get; set; }
        public List<LinkInfo> Links { get; set; }
    }

    public class Aboutme
    {
        public string Title { get; set; }
        public string AboutmeImg { get; set; }
        public string Description { get; set; }
        public List<Highlight> highlights { get; set; }
        public string GotoLink { get; set; }
    }

    public class Skill
    {
        public string Title { get; set; }
        public string PlaceholderSearch { get; set; }
        public string TextSearch { get; set; }
        public string AllInfo { get; set; }
        public List<ItemOption> Categories { get; set; }
        public List<SkillItem> Items { get; set; }
        public string GotoLink { get; set; }
    }

    public class Project
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public string SelecAllText { get; set; }
        public List<CardInfoItem> Items { get; set; }
        public string GotoLink { get; set; }
    }

    public class Contactme
    {
        public string Caption { get; set; }
        public string GotoLink { get; set; }
        public List<ContactInfo> Contacts { get; set; }
    }
    public class ContactInfo
    {
        public string Header { get; set; }
        public string LinkName { get; set; }
        public string Link { get; set; }
        public string IconImg { get; set; }
        public string SecondaryImg { get; set; }
    }


    public class SkillItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string IconImg { get; set; }
    }

    public class LinkInfo
    {
        public string LinkName { get; set; }
        public string Link { get; set; }
    }

    public class Highlight
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public string IconImg { get; set; }
        public string CssClass { get; set; }
    }
}
