using MauiMaps;
using MauiMaps.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MauiKit.Views.DemoApp;

public partial class MapTest : ContentPage
{
	public MapTest()
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
        MyMap.Pins.Add(customPin);
        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(customPin.Location, Distance.FromKilometers(1)));
    }
}