namespace MauiKit.Views.Auth;

public partial class PhoneLoginPage : ContentPage
{
	public PhoneLoginPage()
	{
		InitializeComponent();
		this.BindingContext = new ViewModels.Auth.PhoneLoginViewModel();
    }
}