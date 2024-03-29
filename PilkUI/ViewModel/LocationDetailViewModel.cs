﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PilkUI.Rest;
using PilkUI.Rest.Models;
using System.Collections.ObjectModel;
using Location = PilkUI.Rest.Models.Location;

namespace PilkUI.ViewModel
{
    [QueryProperty("Location", nameof(Location))]
    internal partial class LocationDetailViewModel: ObservableObject, IQueryAttributable
    {
        // Query Property
        [ObservableProperty]
        private Location? location;

        // Post-Query Properties
        [ObservableProperty]
        Location? parent;
        [ObservableProperty]
        ObservableCollection<Location>? children = [];
        [ObservableProperty]
        Location? selectedChild;
        [ObservableProperty]
        ObservableCollection<Pilk>? items = [];
        [ObservableProperty]
        Pilk? selectedPilk;

        public bool HasParent => Parent != null;
        public bool HasChildren => Children?.Count > 0;
        public bool NotHasPilk => Items?.Count < 1;

        private readonly RestService _server = RestService.Instance;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Location", out object? value))
            {
                var queryLoc = value as Location ?? throw new NullReferenceException();
                var loc = await _server.GetLocationFromPkAsync(queryLoc.Pk) ?? throw new NullReferenceException();
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
                await RefreshItems();
            }
            OnPropertyChanged(nameof(Parent));
            OnPropertyChanged(nameof(Children));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(HasChildren));
            OnPropertyChanged(nameof(HasParent));
            OnPropertyChanged(nameof(NotHasPilk));
        }

        [RelayCommand]
        async Task GoToParent()
        { 
            if (Parent is null) return;
            //await Shell.Current.GoToAsync("Loading");
            await Shell.Current.GoToAsync("///Locations/Details", true, new() { { nameof(Location), Parent } });
        }

        [RelayCommand]
        async Task GoToChild()
        {
            if (SelectedChild is null) return;
            //await Shell.Current.GoToAsync("Loading");
            await Shell.Current.GoToAsync("///Locations/Details", true, new() { { nameof(Location), SelectedChild } });
        }

        [RelayCommand]
        async Task GoToPilk()
        {
            if (SelectedPilk is null) return;
            await Shell.Current.GoToAsync("///Locations/PilkDetails", true, new() { { nameof(Pilk), SelectedPilk } });
        }

        [RelayCommand]
        async Task Update()
        {
            if (Location is null) return;
            await Shell.Current.GoToAsync("///Locations/Update", true, new() { { nameof(Location), Location } });
        }

        [RelayCommand]
        async Task CreatePilk()
        {
            if (Location is null) return;
            await Shell.Current.GoToAsync("///Locations/CreatePilk", true, new() { { nameof(Location), Location } });
        }

        internal async Task<bool> UploadImage(FileResult image)
        {
            if (Location is null) return false;
            var ret = await _server.UpdateLocationImageAsync(Location, image);
            if (ret is null)
            {
                return false;
            }
            Location = ret;
            return true;
        }

        internal async Task RefreshItems()
        {
            ObservableCollection<Pilk> items = [];
            if (Location is null) return;
            foreach (var item in Location.Items)
            {
                var pilk = await _server.GetPilkFromPkAsync(item);
                if (pilk is not null)
                    items.Add(pilk);
            }
            Items?.Clear();
            Items = items;
        }
    }
}
