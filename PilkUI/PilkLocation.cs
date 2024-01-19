namespace PilkUI
{
    public class PilkLocation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PilkLocation() 
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}