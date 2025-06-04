
using LiveChartsCore.Measure;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
namespace MauiKit.Views.DemoApp;

public partial class MapUser : ContentPage
{
 
    private List<CustomMapPin> _pins;
    public List<CustomMapPin> Pins
    {
        get { return _pins; }
        set { _pins = value; OnPropertyChanged(); }
    }
    public MapUser()
    {
        InitializeComponent();

        //var pin = new Pin
        //{
        //    Label = "Vị trí của tôi",
        //    Location = new Location(21.0285, 105.8542),
        //    Type = PinType.Place,


        //};
        //MyMapUser.Pins.Add(pin);
        //MyMapUser.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Location, Distance.FromKilometers(1)));
        BindingContext = this;

        Pins = new List<CustomMapPin>()
        {
            new CustomMapPin(PinClicked)
            {
                Id = Guid.NewGuid().ToString(),
                Position = new Location(21.028511, 105.854444),
                Icon = "darkmode"
            },
            new CustomMapPin(PinClicked)
            {
                Id = Guid.NewGuid().ToString(),
                Position = new Location(21.032511, 105.860444),
                Icon = "darkmode"
            }
        };
    }
    private void PinClicked(CustomMapPin pin)
    {
        // Xử lý sự kiện khi click vào pin
        DisplayAlert("Thông báo", $"Bạn vừa click vào pin có Id: {pin.Id}", "OK");
    }
}