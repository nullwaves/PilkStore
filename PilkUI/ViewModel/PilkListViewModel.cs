using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using System.Collections.ObjectModel;
using Pilk = PilkUI.Rest.Models.Pilk;

namespace PilkUI.ViewModel
{
    internal partial class PilkListViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Pilk> pilks;

        [ObservableProperty]
        string searchText;

        [RelayCommand]
        static async Task GoToCreate()
        {
            await Shell.Current.GoToAsync("/Create");
        }

        public async Task RefreshPilks()
        {
            Pilks.Clear();
            var locList = await RestService.Instance.GetPilksAsync(SearchText);
            if (locList is not null)
            {
                foreach (var loc in locList)
                    Pilks.Add(loc);
                OnPropertyChanged(nameof(Pilks));
            }
            else
            {
                await Shell.Current.DisplayAlert("API Error", "Error fetching list of Pilks.", "Okay");
            }
        }

        public PilkListViewModel()
        {
            Pilks = [];
            searchText = string.Empty;
        }
    }
}
