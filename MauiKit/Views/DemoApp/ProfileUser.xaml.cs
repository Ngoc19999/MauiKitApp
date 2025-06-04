using MauiKit.Services;
using MauiKit.ViewModels.DemoApp;

namespace MauiKit.Views.DemoApp;

public partial class ProfileUser : ContentPage
{

    public ProfileUser()
    {
        InitializeComponent();
        BindingContext = UserSession.CurrentUser;
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirmed = await App.Current.MainPage.DisplayAlert("Xác nhận",
                                                                 "Bạn có chắc chắn muốn đăng xuất?",
                                                                 "Đăng xuất",
                                                                 "Hủy");
        if (!confirmed)
            return;     
        UserSession.CurrentUser = null;
        App.Current.MainPage = new NavigationPage(new LoginPage());
    }
    private async void UpdateProfileUserClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new UpdateUser());
    }
    private async void OnProfileUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserInfo());
    }
}