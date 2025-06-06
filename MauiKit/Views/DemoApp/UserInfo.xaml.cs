namespace MauiKit.Views.DemoApp;
using MauiKit.Services;

public partial class UserInfo : ContentPage
{
	public UserInfo()
	{
		InitializeComponent();
        BindingContext = UserSession.CurrentUser;
    }
    private async void NameLabel(object sender, EventArgs e)
    {
      
        await DisplayAlert("Popup", "Mở popup cập nhật tài khoản người dùng", "OK");
    }

    private async void EmailLabel(object sender, EventArgs e)
    {
       
        await DisplayAlert("Popup", "Mở popup cập nhật thông tin người dùng", "OK");
    }
    private async void PhoneLabel(object sender, EventArgs e)
    {

        await DisplayAlert("Popup", "Mở popup cập nhật thông tin người dùng", "OK");
    }
}