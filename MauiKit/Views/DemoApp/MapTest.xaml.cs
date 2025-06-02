using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MauiKit.Views.DemoApp;

public partial class MapTest : ContentPage
{
	public MapTest()
	{
		InitializeComponent();

        var pin = new Pin
        {
            Label = "Vị trí của tôi",
            Location = new Location(21.0285, 105.8542), 
            Type = PinType.Place

        };

        MyMap.Pins.Add(pin);
        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Location, Distance.FromKilometers(1)));
    }
}