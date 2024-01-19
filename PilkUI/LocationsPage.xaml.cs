using PilkUI.Rest;

namespace PilkUI;

public partial class LocationsPage : ContentPage
{
	public List<Location> Locations { get; set; }

	public LocationsPage()
	{
		InitializeComponent();
		Locations = new();
		RefreshLocations();
		BindingContext = this;
        LocationListView.ItemTapped += LocationListView_ItemTapped;
	}

	private async void RefreshLocations()
	{
        Locations = await RestService.Instance.GetLocationsAsync();
		OnPropertyChanged(nameof(Locations));
    }

    private async void LocationListView_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
		var location = e.Item as Location;
		if (location == null) return;
		await Shell.Current.GoToAsync("/Details", new Dictionary<string, object>() { { nameof(Location), location } });
    }
}