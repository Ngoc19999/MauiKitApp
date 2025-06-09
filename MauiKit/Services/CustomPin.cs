namespace MauiMaps.Services;

using Microsoft.Maui.Controls.Maps;

public class CustomPin : Pin, INotifyPropertyChanged
{
    private static int GetDefaultImageHeight()
    {
        return DeviceInfo.Platform == DevicePlatform.iOS ? 35 : 80;
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CustomPin));

    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty ImageHeightProperty =
        BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CustomPin), GetDefaultImageHeight());

    public int ImageHeight
    {
        get => (int)GetValue(ImageHeightProperty);
        set => SetValue(ImageHeightProperty, value);
    }

    public static readonly BindableProperty ImageWidthProperty =
       BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CustomPin), GetDefaultImageHeight());

    public int ImageWidth
    {
        get => (int)GetValue(ImageWidthProperty);
        set => SetValue(ImageWidthProperty, value);
    }

    private Location _location;
    public Location Location
    {
        get => _location;
        set
        {
            if (_location != value)
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public string UserId { get; set; }
}