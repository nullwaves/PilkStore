using PilkUI.ViewModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI;

public partial class LocationsPage : ContentPage
{
	public LocationsPage()
	{
		InitializeComponent();
        LocationListView.ItemTapped += LocationListView_ItemTapped;
        NavigatedTo += LocationsPage_NavigatedTo;
        SearchInput.TextChanged += SearchInput_TextChanged;
	}

    private async void SearchInput_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (BindingContext is LocationListViewModel vm)
            await vm.RefreshLocations();
    }

    private async void LocationsPage_NavigatedTo(object? sender, NavigatedToEventArgs e)
    {
        if (BindingContext is LocationListViewModel vm)
		    await vm.RefreshLocations();
    }

    private async void LocationListView_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Location location) return;
        await Shell.Current.GoToAsync("/Details", true, new() { { nameof(Location), location } });
    }
}