namespace PilkUI.Rest.Models
{
    public class Location

    {
        public int Pk { get; set; }
        public int? Parent { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri? Image { get; set; }
        public List<int> Children { get; set; }

        public Location() 
        {
            Name = string.Empty;
            Description = string.Empty;
            Children = [];
        }
    }
}