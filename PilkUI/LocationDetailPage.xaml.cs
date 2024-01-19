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
}