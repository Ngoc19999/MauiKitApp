using MauiMaps;
using MauiMaps.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System;
namespace MauiKit.Views.DemoApp;

public partial class MapUser : ContentPage
{
    
    public MapUser()
    {
        InitializeComponent();
        var customPin = new CustomPin
        {
            Label = "Vị trí tùy chỉnh",
            Address = "123 Đường ABC",
            Type = PinType.Generic,
            // Ví dụ: tọa độ của Hà Nội
            Location = new Location(21.0278, 105.8342),
            // Đảm bảo file ảnh "custom_pin.png" có trong Resources/Images và được cấu hình đúng
            ImageSource = ImageSource.FromFile("call.png")
        };
        MyMapUser.Pins.Add(customPin);
        MyMapUser.MoveToRegion(MapSpan.FromCenterAndRadius(customPin.Location, Distance.FromKilometers(1)));
        //var pin = new Pin
        //{
        //    Label = "Vị trí của tôi",
        //    Location = new Location(21.0285, 105.8542),
        //    Type = PinType.Place,
        //};
        //MyMapUser.Pins.Add(pin);
        //MyMapUser.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Location, Distance.FromKilometers(1)));
    }
   

}