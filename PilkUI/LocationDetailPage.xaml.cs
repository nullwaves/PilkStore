using PilkUI.Rest;
using PilkUI.ViewModel;

namespace PilkUI;

public partial class LocationDetailPage : ContentPage
{
	public LocationDetailPage()
	{
		InitializeComponent();
	}

    private async void DeleteLocation_Clicked(object sender, EventArgs e)
    {
		var response = await DisplayAlert("Confirm Deletion", "Are you sure you would like to delete this location?", "Yes", "No");
		if (response)
		{
			// Delete Location
			if (BindingContext is LocationDetailViewModel vm)
			{
				var result = await RestService.Instance.DeleteLocationAsync(vm.Location);
				if (result)
				{
					await Shell.Current.GoToAsync("///Locations");
				}
				else
				{
					await DisplayAlert("API Error", "Failed to delete location.", "Okay");
				}
			}
		}
    }
}