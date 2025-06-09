using Microsoft.Maui.Maps;

namespace MauiKit.Views;

public partial class LocationMapPage : ContentPage
{
	public LocationMapPage()
	{
		InitializeComponent();
		this.BindingContext = new LocationMapViewModel();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is LocationMapViewModel vm)
        {
            vm.StopAllListeners();
        }
    }



    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is LocationMapViewModel vm)
        {
            if (vm.UserMarkers!=null && vm.UserMarkers.Count > 0)
            {
                var local = vm.UserMarkers.FirstOrDefault().Location;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(local, new Microsoft.Maui.Maps.Distance(3000)));
            }
        }
    }                       
}