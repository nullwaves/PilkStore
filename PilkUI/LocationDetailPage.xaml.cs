using PilkUI.Rest;
using PilkUI.ViewModel;

namespace PilkUI;

public partial class LocationDetailPage : ContentPage
{
    public LocationDetailPage()
    {
        InitializeComponent();
    }

    private async void DeleteLocation_Clicked(object sender, EventArgs e)
    {

        if (BindingContext is LocationDetailViewModel vm)
        {
            var message = "Are you sure you would like to delete this location?";
            if (vm.Location.Children.Count > 0)
                message += $" This will delete {vm.Location.Children.Count} sub-locations.";
            var response = await DisplayAlert("Confirm Deletion", message, "Yes", "No");
            if (response)
            {
                // Delete Location
                var result = await RestService.Instance.DeleteLocationAsync(vm.Location);
                if (result)
                {
                    await Shell.Current.GoToAsync("///Locations");
                }
                else
                {
                    await DisplayAlert("API Error", "Failed to delete location.", "Okay");
                }
            }
        }
    }

    private async void AddImage_Clicked(object sender, EventArgs e)
    {
        if(BindingContext is LocationDetailViewModel vm)
        {
            var loc = vm.Location;
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
        if (BindingContext is LocationDetailViewModel vm)
        {
            var loc = vm.Location;
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
}