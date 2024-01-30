using Location = PilkUI.Rest.Models.Location;

namespace PilkUI;

public partial class LocationsPage : ContentPage
{
	public LocationsPage()
	{
		InitializeComponent();
        LocationListView.ItemTapped += LocationListView_ItemTapped;
        // NavigatedTo += LocationsPage_NavigatedTo;
	}

    private async void LocationsPage_NavigatedTo(object? sender, NavigatedToEventArgs e)
    {
		//await (BindingContext as LocationListViewModel).RefreshLocations();
    }

    private async void LocationListView_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Location location) return;
        await Shell.Current.GoToAsync("/Details", true, new() { { nameof(Location), location } });
    }
}