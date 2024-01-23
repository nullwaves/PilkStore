using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Location", nameof(Location))]
    internal partial class LocationDetailViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private Location location = new();

        [ObservableProperty]
        Location? parent;
        [ObservableProperty]
        List<Location>? children = [];

        public bool HasParent => Parent != null;
        public bool HasChildren => Children?.Count > 0;

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
                if (Location.Parent != null)
                {
                    Parent = await RestService.Instance.GetLocationFromUriAsync(Location.Parent);
                }
                Children = [];
                foreach (var link in Location.Children)
                {
                    var child = await RestService.Instance.GetLocationFromUriAsync(link);
                    if (child is not null)
                        Children.Add(child);
                }
            }
            OnPropertyChanged(nameof(Children));
        }

        [RelayCommand]
        async Task GoToParent()
        { 
            if (Parent is null) return;
            await Shell.Current.GoToAsync("../Details", true, new Dictionary<string, object>() { { nameof(Location), Parent } });
        }
    }
}
