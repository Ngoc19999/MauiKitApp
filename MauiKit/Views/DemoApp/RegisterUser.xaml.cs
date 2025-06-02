using MauiKit.Models;
using MauiKit.Services;
using System.Text.RegularExpressions;
using Microsoft.Maui.Graphics;
namespace MauiKit.Views.DemoApp;

public partial class RegisterUser : ContentPage
{
    private readonly ApiServiceBase _apiServiceBase;
    private string _accountType;
    public RegisterUser()
    {
        InitializeComponent();
        _apiServiceBase = new ApiServiceBase();
    }

    private void OnSelectParentClicked(object sender, EventArgs e)
    {
        _accountType = "parent";
        BtnParent.BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
        BtnChild.BackgroundColor = Color.FromHex("#808080");
    }

    // Xử lý sự kiện chọn tài khoản con
    private void OnSelectChildClicked(object sender, EventArgs e)
    {
        _accountType = "child";
        BtnChild.BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
        BtnParent.BackgroundColor = Color.FromHex("#808080");
    }
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Lấy dữ liệu nhập từ các Entry
        var fullName = EntryFullName.Text;
        var phoneNumber = EntryPhone.Text;
        var email = EntryEmail.Text;
        var password = EntryPass.Text;
        var confirmPassword = EntryCoffirmPass.Text;
        var referralCode = EntryReferralCode.Text;

        // Kiểm tra đầu vào
        if (string.IsNullOrWhiteSpace(fullName))
        {
            await DisplayAlert("Error", "Vui lòng nhập Họ và Tên", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            await DisplayAlert("Error", "Vui lòng nhập Số điện thoại", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Vui lòng nhập Email", "OK");
            return;
        }

        // Kiểm tra định dạng email, cho phép @...com hoặc @...net
        string pattern = @"^[\w\-.]+@[\w\-.]+\.(com|net)$";
        if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
        {
            await DisplayAlert("Error", "Địa chỉ email không hợp lệ. Vui lòng nhập email có định dạng @...com hoặc @...net", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Vui lòng nhập Mật khẩu", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Vui lòng nhập lại Mật khẩu", "OK");
            return;
        }
        if (!password.Equals(confirmPassword))
        {
            await DisplayAlert("Error", "Xác nhận mật khẩu không đúng", "OK");
            return;
        }

        // Tạo đối tượng DTO đăng ký dựa trên thông tin nhập
        var registerDto = new RegisterUserDto
        {
            HoTen = fullName,
            Phone = phoneNumber,
            Password = password,
            // Nếu mã giới thiệu để trống thì gán giá trị mặc định "0"
            RefId = string.IsNullOrWhiteSpace(referralCode) ? "0" : referralCode,
            Email = email,
            NgaySinh = null,
            GioiTinh = 0,
            DiaChi = "",
            Avatar = "",
            Gplxb2 = "",
            Cccd = "",
            Lltp = "",
            Gksk = "",
            Point = 0,
            Money = 0,
            Status = 1,
            Style = _accountType == "parent" ? (byte)1 : (byte)2,
            IsDeleted = false
        };

        // Gọi service để thực hiện đăng ký
        var response = await _apiServiceBase.RegisterUserAsync(registerDto);
        if (response != null && response.Code == 0 && response.Data != 0)
        {
            // Đăng ký thành công, thông báo và chuyển trang
            await DisplayAlert("Thông báo", "Đăng ký thành công!", "OK");
            await Navigation.PushAsync(new SigninPage());
        }
        if (response.Data == 0)
        {
            // Đăng ký không thành công, hiện thông báo lỗi
            string errorMsg = response?.Message ?? "Đăng ký không thành công. Kiểm tra số điện thoại hoặc email.";
            await DisplayAlert("Error", errorMsg, "OK");
        }
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        // Chuyển hướng đến trang đăng nhập
        await Navigation.PushAsync(new SigninPage());
    }
}
