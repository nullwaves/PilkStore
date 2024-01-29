using PilkUI.Rest;
using PilkUI.ViewModel;

namespace PilkUI;

public partial class PilkDetailPage : ContentPage
{
	public PilkDetailPage()
	{
        InitializeComponent();
	}

    private async void AddImage_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is PilkDetailViewModel vm)
        {
            FileResult image = await MediaPicker.Default.PickPhotoAsync();

            if (image is not null)
            {
                if (!await vm.UploadImage(image))
                {
                    await DisplayAlert("API Error", "Failed to upload image.", "Okay");
                }
            }
        }
    }

    private async void CaptureImage_Clicked(object sender, EventArgs e)
    {
        if (!MediaPicker.Default.IsCaptureSupported)
        {
            await DisplayAlert("Error", "Image capture not supported on this device.", "Okay");
            return;
        }
        if (BindingContext is PilkDetailViewModel vm)
        {
            FileResult image = await MediaPicker.Default.CapturePhotoAsync();

            if (image is not null)
            {
                if (!await vm.UploadImage(image))
                {
                    await DisplayAlert("API Error", "Failed to upload image.", "Okay");
                }
            }
        }
    }

    private async void DeletePilk_Clicked(object sender, EventArgs e)
    {

        if (BindingContext is PilkDetailViewModel vm && vm.Pilk is not null)
        {
            var message = "Are you sure you would like to delete this Pilk?";
            var response = await DisplayAlert("Confirm Deletion", message, "Yes", "No");
            if (response)
            {
                // Delete Location
                var result = await RestService.Instance.DeletePilkAsync(vm.Pilk);
                if (result)
                {
                    if (vm.Location is null)
                        await Shell.Current.GoToAsync("///Locations");
                    else
                        await Shell.Current.GoToAsync("///Locations/Details", true, new() { { "Location", vm.Location } });
                }
                else
                {
                    await DisplayAlert("API Error", "Failed to delete location.", "Okay");
                }
            }
        }
    }
}