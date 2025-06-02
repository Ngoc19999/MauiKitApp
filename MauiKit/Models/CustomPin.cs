using Microsoft.Maui.Controls.Maps;

public class CustomPin : Pin
{
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource), // Tên thuộc tính
        typeof(ImageSource), // Kiểu dữ liệu của thuộc tính
        typeof(CustomPin) // Lớp chứa thuộc tính
    );

    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
}
