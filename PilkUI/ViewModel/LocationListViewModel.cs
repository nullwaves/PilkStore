using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using System.Collections.ObjectModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    internal partial class LocationListViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Location> locations;

        [RelayCommand]
        static async Task GoToCreate()
        {
            await Shell.Current.GoToAsync("/Create");
        }

        public async Task RefreshLocations()
        {
            Locations.Clear();
            var locList = await RestService.Instance.GetLocationsAsync();
            if (locList is not null)
            {
                foreach (var loc in locList)
                    Locations.Add(loc);
                OnPropertyChanged(nameof(Locations));
            }
            else
            {
                await Shell.Current.DisplayAlert("API Error", "Error fetching list of Locations.", "Okay");
            }
        }

        public LocationListViewModel()
        {
            Locations = [];
        }
    }
}
