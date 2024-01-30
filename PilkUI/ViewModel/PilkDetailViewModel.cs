using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using PilkUI.Rest.Models;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Pilk", nameof(Pilk))]
    internal partial class PilkDetailViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        Pilk? pilk;
        [ObservableProperty]
        Location? location;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Pilk", out object? value) && value is Pilk item)
            {
                Pilk = item;
                Location = await RestService.Instance.GetLocationFromPkAsync(item.Location);
            }
        }

        [RelayCommand]
        async Task GoToLocation()
        {
            if (Location is null) return;
            await Shell.Current.GoToAsync("///Locations/Details", true, new() { { "Location", Location } });
        }

        [RelayCommand]
        async Task Update()
        {
            if (Pilk is null) return;
            await Shell.Current.GoToAsync("///Locations/PilkUpdate", true, new() { { nameof(Pilk), Pilk } });
        }

        internal async Task<bool> UploadImage(FileResult image)
        {
            if (Pilk is null) return false;
            var ret = await RestService.Instance.UpdatePilkImageAsync(Pilk, image);
            if (ret is null)
            {
                return false;
            }
            Pilk = ret;
            return true;
        }
    }
}
