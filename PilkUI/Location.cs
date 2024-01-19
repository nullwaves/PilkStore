namespace PilkUI
{
    public class Location

    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Location() 
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}