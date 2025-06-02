
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Devices.Sensors;
namespace MauiKit.Views.DemoApp;

public partial class MapUser : ContentPage
{
    public MapUser()
    {
        InitializeComponent();

        var pin = new Pin
        {
            Label = "Vị trí của tôi",
            Location = new Location(21.0285, 105.8542),
            Type = PinType.Place

        };
        MyMapUser.Pins.Add(pin);
        MyMapUser.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Location, Distance.FromKilometers(1)));
    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        await UpdateLocationOnMapAsync();
    }
    private async Task UpdateLocationOnMapAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);
            if (location != null)
            {
                var newPin = new Pin
                {
                    Label = "Vị trí cập nhật",
                    Location = new Location(location.Latitude, location.Longitude),
                    Type = PinType.Place
                };
                MyMapUser.Pins.Clear();
                MyMapUser.Pins.Add(newPin);
                MyMapUser.MoveToRegion(MapSpan.FromCenterAndRadius(newPin.Location, Distance.FromKilometers(1)));
            }
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            await DisplayAlert("Lỗi", "Thiết bị của bạn không hỗ trợ định vị.", "OK");
        }
        catch (PermissionException pEx)
        {
            await DisplayAlert("Lỗi", "Ứng dụng không có quyền truy cập vị trí.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Không thể lấy vị trí: {ex.Message}", "OK");
        }
    }

}