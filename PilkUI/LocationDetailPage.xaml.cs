using Microsoft.Maui.Devices.Sensors;
using PilkUI.Rest;

namespace PilkUI;

[QueryProperty("Location", nameof(PilkUI.Location))]
public partial class LocationDetailPage : ContentPage
{
	Location _location;
	public Location Location
	{
		get => _location;
		set
		{
			_location = value;
			OnPropertyChanged();
		}
	}

	public LocationDetailPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private async void ParentButton_Clicked(object sender, EventArgs e)
    {
		if (Location.Parent is null) return;
		var location = await RestService.Instance.GetLocationFromUriAsync(Location.Parent);
		if (location is null) return;
        await Shell.Current.GoToAsync("../Details", true, new Dictionary<string, object>() { { nameof(PilkUI.Location), location } });
    }
}