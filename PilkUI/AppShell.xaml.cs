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
            Routing.RegisterRoute("Locations/Update", typeof(LocationUpdatePage));
            Routing.RegisterRoute("Locations/CreatePilk", typeof(PilkCreatePage));
            Routing.RegisterRoute("Locations/PilkDetails", typeof(PilkDetailPage));
        }
    }
}
