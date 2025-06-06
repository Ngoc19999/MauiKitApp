using MauiKit.Services;
using MauiKit.ViewModels.DemoApp;
using Newtonsoft.Json.Linq;

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

    private void BtnParent_Clicked(object sender, EventArgs e)
    {
        // Khi chọn "Thông Tin Cá Nhân", hiển thị khối cá nhân và ẩn khối tài khoản
        AccountInfo.IsVisible = true;
        Account.IsVisible = false;
    }
    private void BtnChild(object sender, EventArgs e)
    {
        // Khi chọn "Thông Tin Cá Nhân", hiển thị khối cá nhân và ẩn khối tài khoản
        AccountInfo.IsVisible = false;
        Account.IsVisible = true;
    }

    private async void NameLabel(object sender, EventArgs e)
    {

        await DisplayAlert("Popup", "Mở popup cập nhật tài khoản người dùng", "OK");
    }

    private async void BirthdayLabel(object sender, EventArgs e)
    {

        await DisplayAlert("Popup", "Mở popup cập nhật thông tin người dùng", "OK");
    }
    private async void AddressLabel(object sender, EventArgs e)
    {

        await DisplayAlert("Popup", "Mở popup cập nhật thông tin người dùng", "OK");
    }

    
}