using System.Diagnostics;

namespace PilkUI;

public partial class LocationDetailPage : ContentPage
{
	public LocationDetailPage()
	{
		InitializeComponent();
        PropertyChanged += LocationDetailPage_PropertyChanged;
	}

    private void LocationDetailPage_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Debug.WriteLine("jfl;sjlk;aj");
    }
}