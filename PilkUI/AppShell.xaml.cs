namespace PilkUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Loading", typeof(LoadingPage));
            Routing.RegisterRoute("Locations/Details", typeof(LocationDetailPage));
            Routing.RegisterRoute("Locations/Create", typeof(LocationCreatePage));
        }
    }
}
