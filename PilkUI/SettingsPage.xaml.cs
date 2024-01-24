using CommunityToolkit.Mvvm.Input;

namespace PilkUI;

public partial class SettingsPage : ContentPage
{
	public string Server { get; set; }

	public SettingsPage()
	{
		InitializeComponent();
		Server = SettingsService.GetPilkServer();
		BindingContext = this;
	}

	[RelayCommand]
	async Task SaveSettings()
	{
		if (Server != null)
		{
			SettingsService.SetPilkServer(Server);
			await Shell.Current.GoToAsync("///Locations");
		}
	}
}