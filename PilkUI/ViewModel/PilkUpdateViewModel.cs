using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using PilkUI.Rest.Models;
using System.Collections.ObjectModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Pilk", nameof(Pilk))]
    internal partial class PilkUpdateViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        Pilk pilk;
        [ObservableProperty]
        ObservableCollection<Location> locations;
        [ObservableProperty]
        Location? selectedLocation;

        [RelayCommand]
        async Task SavePilk()
        {
            Pilk.Location = SelectedLocation is Location loc ? loc.Pk : Pilk.Location;
            var response = await RestService.Instance.UpdatePilkAsync(Pilk);
            if (response is not null)
            {
                await Shell.Current.GoToAsync("///Locations/PilkDetails", true, new() { { nameof(Pilk), response } });
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Pilk", out object? value) && value is Pilk item)
            {
                Pilk = item;
                await RefreshLocations();
            }
        }

        async Task RefreshLocations()
        {
            Locations.Clear();
            var resp = await RestService.Instance.GetLocationsAsync();
            if (resp is null) return;
            foreach (var loc in resp)
                Locations.Add(loc);
        }

#nullable disable
        public PilkUpdateViewModel()
        {
            Locations = [];
        }
#nullable enable
    }
}
