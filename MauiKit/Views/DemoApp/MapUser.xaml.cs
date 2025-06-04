
using LiveChartsCore.Measure;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
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
            Type = PinType.Place,


        };
        MyMapUser.Pins.Add(pin);
        MyMapUser.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Location, Distance.FromKilometers(1)));

    }
}