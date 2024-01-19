namespace PilkUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Locations/Details", typeof(LocationDetailPage));
        }
    }
}
