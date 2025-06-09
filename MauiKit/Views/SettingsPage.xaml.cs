namespace MauiKit.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
		this.BindingContext = new ViewModels.SettingsViewModel();
    }
}