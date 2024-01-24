using CommunityToolkit.Mvvm.ComponentModel;

namespace PilkUI.ViewModel
{
    internal partial class LocationCreateViewModel: ObservableObject
    {
        [ObservableProperty]
        string? name;
        [ObservableProperty]
        string? description;
    }
}
