
using System.Windows.Input;

namespace MauiKit.Controls;

public partial class ButtonLoading : Frame
{
    public ButtonLoading()
    {
        InitializeComponent();
    }
    public event EventHandler<EventArgs> Tapped;

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        propertyName: nameof(Command),
        returnType: typeof(ICommand),
        declaringType: typeof(ButtonLoading),
        defaultBindingMode: BindingMode.TwoWay
        );

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty); set { SetValue(CommandProperty, value); }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(ButtonLoading),
        defaultValue: "",
        defaultBindingMode: BindingMode.TwoWay
        );

    public string Text
    {
        get => (string)GetValue(TextProperty); set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        defaultValue: "Chờ giây lát...",
        declaringType: typeof(ButtonLoading),
        defaultBindingMode: BindingMode.OneWay
        );

    public string LoadingText
    {
        get => (string)GetValue(LoadingTextProperty); set { SetValue(LoadingTextProperty, value); }
    }

    public static readonly BindableProperty IsInProgressProperty = BindableProperty.Create(
        propertyName: nameof(IsInProgress),
        returnType: typeof(bool),
        defaultValue: false,
        declaringType: typeof(ButtonLoading),
        propertyChanged: IsInProgressPropertyChanged
        );

    private static void IsInProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (ButtonLoading)bindable;
        if (newValue != null)
        {
            bool isInProgress = (bool)newValue;
            if (isInProgress)
            {
                controls.lblButtonText.Text = controls.LoadingText;
                controls.lblButtonText.TextColor = Color.FromArgb("#ffffff");
            }

            else
                controls.lblButtonText.Text = controls.Text;
        }
    }

    public bool IsInProgress
    {
        get => (bool)GetValue(IsInProgressProperty); set { SetValue(IsInProgressProperty, value); }
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Tapped?.Invoke(sender, e);
    }
}