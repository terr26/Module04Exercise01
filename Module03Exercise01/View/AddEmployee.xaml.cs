using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel; // For Connectivity
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Module03Exercise01.View;

public partial class AddEmployee : ContentPage
{
    public AddEmployee()
    {
        InitializeComponent();
    }

    // This method updates the full name whenever first or last name changes
    private void OnNameChanged(object sender, TextChangedEventArgs e)
    {
        FullNameLabel.Text = $"{FirstNameEntry.Text} {LastNameEntry.Text}".Trim();
    }

    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        // Display the user-input values directly
        MunicipalityEntry.Text = string.IsNullOrWhiteSpace(MunicipalityEntry.Text) ? "Unknown Municipality" : MunicipalityEntry.Text;
        ProvinceEntry.Text = string.IsNullOrWhiteSpace(ProvinceEntry.Text) ? "Unknown Province" : ProvinceEntry.Text;

        // Optionally show that you are using user input
        AddressLabel.Text = $"Address: {MunicipalityEntry.Text}, {ProvinceEntry.Text}";


        // Get GPS location only if you want to still provide coordinates
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.High
                });
            }

            if (location != null)
            {
                CoordinatesLabel.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
                CoordinatesLabel2.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
            }
            else
            {
                CoordinatesLabel.Text = "Unable to get location";
            }
        }
        catch (Exception ex)
        {
            LocationLabel.Text = $"Error: {ex.Message}";
        }
    }


    private async void OnCapturePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Capture a photo using MediaPicker
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    await LoadPhotoAsync(photo);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    // Load photo and display it in the Image control
    private async Task LoadPhotoAsync(FileResult photo)
    {
        if (photo == null)
            return;

        Stream stream = await photo.OpenReadAsync();
        CaptureImage.Source = ImageSource.FromStream(() => stream);
    }
}
