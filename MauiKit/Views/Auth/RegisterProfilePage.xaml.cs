namespace MauiKit.Views.Auth;

public partial class RegisterProfilePage : ContentPage
{
	public RegisterProfilePage()
	{
		InitializeComponent();
		this.BindingContext = new ViewModels.Auth.RegisterProfileViewModel();
    }
}