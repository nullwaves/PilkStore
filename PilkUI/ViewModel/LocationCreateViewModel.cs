using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics;
using PilkUI.Rest;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    internal partial class LocationCreateViewModel: ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;

        public LocationCreateViewModel()
        {
            Name = "";
            Description = "";
        }

        [RelayCommand]
        async Task SaveLocation()
        {
            if (Name is null || Description is null) return;
            var newLocation = new Location() { Name = Name, Description = Description };
            var response = await RestService.Instance.PostLocation(newLocation);
            if (response is not null)
            {
                await Shell.Current.GoToAsync("///Locations/Details", true, new Dictionary<string, object>() { { nameof(Location), response } });
            }
        }
    }
}
