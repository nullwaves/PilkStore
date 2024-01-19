namespace PilkUI;

[QueryProperty("PLocation", nameof(PilkLocation))]
public partial class LocationDetailPage : ContentPage
{
	PilkLocation pLocation;
	public PilkLocation PLocation
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