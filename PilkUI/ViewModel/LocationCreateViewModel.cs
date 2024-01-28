using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using System.Collections.ObjectModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    internal partial class LocationCreateViewModel: ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;

        [ObservableProperty]
        ObservableCollection<Location> parents;

        [ObservableProperty]
        object? selectedParent;

        public LocationCreateViewModel()
        {
            Name = "";
            Description = "";
            Parents = new();
            RefreshParents();
        }

        private async void RefreshParents()
        {
            Parents.Add(new Location() { Pk = -1, Name = "None"});
            var locs = await RestService.Instance.GetLocationsAsync();
            if (locs is null) return;
            foreach(var loc in locs)
                Parents.Add(loc);
        }

        [RelayCommand]
        async Task SaveLocation()
        {
            if (Name is null || Description is null) return;
            var newLocation = new Location() { Name = Name, Description = Description };
            if (SelectedParent is Location parent && parent.Pk > -1)
                newLocation.Parent = parent.Pk;
            var response = await RestService.Instance.CreateLocationAsync(newLocation);
            if (response is not null)
            {
                await Shell.Current.GoToAsync("///Locations/Details", true, new Dictionary<string, object>() { { nameof(Location), response } });
            }
        }
    }
}
