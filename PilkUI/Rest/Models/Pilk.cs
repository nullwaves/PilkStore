namespace PilkUI.Rest.Models
{
    public class Pilk
    {
        public int Pk { get; set; }
        public int Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri? Image {  get; set; }

        public Pilk() 
        {
            Name = "";
            Description = "";
        }
    }
}
