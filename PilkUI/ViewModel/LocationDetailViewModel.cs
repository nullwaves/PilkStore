using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using System.Collections.ObjectModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Location", nameof(Location))]
    internal partial class LocationDetailViewModel: ObservableObject, IQueryAttributable
    {
        // Query Property
        [ObservableProperty]
        private Location location;

        // Post-Query Properties
        [ObservableProperty]
        Location? parent;
        [ObservableProperty]
        ObservableCollection<Location>? children = [];
        [ObservableProperty]
        Location? selectedChild;

        public bool HasParent => Parent != null;
        public bool HasChildren => Children?.Count > 0;

        private RestService _server = RestService.Instance;

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
                var par = Location.Parent;
                Parent = par is null ? null : await _server.GetLocationFromPkAsync((int)par);
                Children = [];
                foreach (var link in Location.Children)
                {
                    var child = await _server.GetLocationFromPkAsync(link);
                    if (child is not null)
                        Children.Add(child);
                }
            }
            OnPropertyChanged(nameof(Parent));
            OnPropertyChanged(nameof(Children));
            OnPropertyChanged(nameof(HasChildren));
            OnPropertyChanged(nameof(HasParent));
        }

        [RelayCommand]
        async Task GoToParent()
        { 
            if (Parent is null) return;
            //await Shell.Current.GoToAsync("Loading");
            await Shell.Current.GoToAsync("///Locations/Details", true, new Dictionary<string, object>() { { nameof(Location), Parent } });
        }

        [RelayCommand]
        async Task GoToChild()
        {
            if (SelectedChild is null) return;
            //await Shell.Current.GoToAsync("Loading");
            await Shell.Current.GoToAsync("///Locations/Details", true, new Dictionary<string, object>() { { nameof(Location), SelectedChild } });
        }

        [RelayCommand]
        async Task Update()
        {
            await Shell.Current.GoToAsync("///Locations/Update", true, new Dictionary<string, object>() { { nameof(Location), Location } });
        }
    }
}
