using PilkUI.ViewModel;
using Pilk = PilkUI.Rest.Models.Pilk;

namespace PilkUI;

public partial class PilkListPage : ContentPage
{
	public PilkListPage()
	{
		InitializeComponent();
        PilkListView.ItemTapped += PilkListView_ItemTapped;
        NavigatedTo += PilksPage_NavigatedTo;
        SearchInput.TextChanged += SearchInput_TextChanged;
	}

    private async void SearchInput_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (BindingContext is PilkListViewModel vm)
            await vm.RefreshPilks();
    }

    private async void PilksPage_NavigatedTo(object? sender, NavigatedToEventArgs e)
    {
        if (BindingContext is PilkListViewModel vm)
		    await vm.RefreshPilks();
    }

    private async void PilkListView_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Pilk Pilk) return;
        await Shell.Current.GoToAsync("/PilkDetails", true, new() { { nameof(Pilk), Pilk } });
    }
}