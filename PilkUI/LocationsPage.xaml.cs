namespace PilkUI;

public partial class LocationsPage : ContentPage
{
	public List<PilkLocation> Locations { get; set; }

	public LocationsPage()
	{
		InitializeComponent();
		Locations = new();
		for (char x = 'A'; x <= 'Z'; x++)
			for (int i = 1; i < 99; i++)
				Locations.Add(new PilkLocation() { ID = x * 100 + i, Name = $"{x}{i:D3}", Description = $"Stuff @ {x}-{i}" });
		BindingContext = this;
        LocationListView.ItemSelected += LocationListView_ItemSelected;
	}

    private async void LocationListView_ItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
		var location = e.SelectedItem as PilkLocation;
		if (location == null) return;
		await Shell.Current.GoToAsync("/Details", new Dictionary<string, object>() { { "PilkLocation", location } });
    }
}