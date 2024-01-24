using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI;

public partial class LocationsPage : ContentPage
{
	public List<Location> Locations { get; set; }

    [RelayCommand]
	async Task GoToCreate()
	{
		await Shell.Current.GoToAsync("/Create");
	}

	public LocationsPage()
	{
		InitializeComponent();
		Locations = [];
		RefreshLocations();
		BindingContext = this;
        LocationListView.ItemTapped += LocationListView_ItemTapped;
	}

	private async void RefreshLocations()
	{
		var loc = await RestService.Instance.GetLocationsAsync();
		if (loc is not null)
		{
			Locations = loc;
			OnPropertyChanged(nameof(Locations));
		}
		else
		{
			await DisplayAlert("API Error", "Error fetching list of Locations.", "Okay");
		}
    }

    private async void LocationListView_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Location location) return;
        await Shell.Current.GoToAsync("/Details", new Dictionary<string, object>() { { nameof(Location), location } });
    }
}