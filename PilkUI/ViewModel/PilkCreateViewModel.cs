using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using PilkUI.Rest.Models;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Location", nameof(Location))]
    internal partial class PilkCreateViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        Location location;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;

        [RelayCommand]
        async Task SavePilk()
        {
            if (Name.Length < 1 || Description.Length < 1 || Location is null) return;
            Pilk item = new() 
            { 
                Location = Location.Pk, 
                Name = Name, 
                Description = Description,
            };
            var response = await RestService.Instance.CreatePilkAsync(item);
            if (response is not null)
            {
                await Shell.Current.GoToAsync(
                    "///Locations/PilkDetails", 
                    true, 
                    new() { { nameof(Pilk), response } });
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Location", out object? value) && value is Location loc)
            {
                Location = loc;
            }
        }

#nullable disable
        public PilkCreateViewModel() 
        {
            Name = "";
            Description = "";
        }
#nullable enable
    }
}
