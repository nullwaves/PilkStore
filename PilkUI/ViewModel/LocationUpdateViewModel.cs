using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using System.Collections.ObjectModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Location", nameof(Location))]
    internal partial class LocationUpdateViewModel : ObservableObject, IQueryAttributable
    {
        // Query Property
        [ObservableProperty]
        private Location location;

        // Parent Choices
        [ObservableProperty]
        ObservableCollection<Location> parents;
        [ObservableProperty]
        object? selectedParent;

#nullable disable
        public LocationUpdateViewModel()
        {
            Parents = new();
        }
#nullable enable

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Location"))
            {
                var loc = query["Location"] as Location;
                if (loc is null)
                {
                    throw new NullReferenceException();
                }
                Location = loc;
                await RefreshParents();
                SelectedParent = loc.Parent;
            }
        }

        private async Task RefreshParents()
        {
            Parents.Add(new Location() { Pk = -1, Name = "None" });
            var locs = await RestService.Instance.GetLocationsAsync();
            if (locs is null) return;
            foreach (var loc in locs)
                Parents.Add(loc);
        }

        [RelayCommand]
        async Task SaveLocation()
        {
            Location.Parent = SelectedParent is Location loc ? loc.Pk : null;
            var response = await RestService.Instance.UpdateLocationAsync(Location);
            if (response is not null)
            {
                await Shell.Current.GoToAsync("///Locations/Details", true, new Dictionary<string, object>() { { nameof(Location), response } });
            }
        }
    }
}
