namespace MauiKit.Views;
public partial class ThemeSettingsPage : BasePage
{
    public ThemeSettingsPage()
    {
        InitializeComponent();
        BindingContext = new ThemeSettingsViewModel();
    }
}
