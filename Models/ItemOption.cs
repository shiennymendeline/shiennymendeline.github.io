namespace shiennymendeline.github.io.Models
{
    public class ItemOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public ItemOption(string id, string name, bool isSelected = false)
        {
            Id = id;
            Name = name;
            IsSelected = isSelected;
        }
    }
}
