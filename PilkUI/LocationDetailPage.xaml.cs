using Microsoft.Maui.Devices.Sensors;
using PilkUI.Rest;

namespace PilkUI;

[QueryProperty("PLocation", nameof(Location))]
public partial class LocationDetailPage : ContentPage
{
	Location pLocation;
	public Location PLocation
	{
		get => pLocation;
		set
		{
			pLocation = value;
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
		Location location = await RestService.Instance.GetLocationFromUriAsync(PLocation.Parent);
        await Shell.Current.GoToAsync("../Details", true, new Dictionary<string, object>() { { nameof(Location), location } });
    }
}