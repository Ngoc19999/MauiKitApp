

namespace MauiKit.Views.DemoApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void Signin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SigninPage());
    }

    private async void Signup_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterUser());
    }
}