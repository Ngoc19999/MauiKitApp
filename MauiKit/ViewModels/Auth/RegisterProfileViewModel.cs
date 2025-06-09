using MauiKit.Services;
using Plugin.Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace MauiKit.ViewModels.Auth
{
    public partial class RegisterProfileViewModel : ObservableObject
    {
        private readonly FirestoreUserService _userService;
        private readonly IFirebaseAuth _auth;

        [ObservableProperty] private string displayName;
        [ObservableProperty] private string? phoneNumber;
        [ObservableProperty] private string email;
        [ObservableProperty] private string address;
        [ObservableProperty] private string avatarUrl = "https://i.pravatar.cc/100"; // avatar mặc định

        public RegisterProfileViewModel()
        {
            _userService = new FirestoreUserService();
            _auth = CrossFirebaseAuth.Current;

            var user = _auth.CurrentUser;

            PhoneNumber = user?.ProviderInfos.FirstOrDefault().PhoneNumber;
            Email = user?.Email ?? string.Empty;
            Address = string.Empty; // có thể để trống hoặc lấy từ một nguồn khác
            AvatarUrl = user?.PhotoUrl;
                
            DisplayName = user?.DisplayName;
        }

        [RelayCommand]
        private async Task CompleteAsync()
        {
            try
            {
                var uid = _auth.CurrentUser?.Uid;
                if (string.IsNullOrEmpty(uid))
                    return;

                var userModel = new UserModel
                {
                    Id = uid,
                    Name = DisplayName,
                    Email = Email,
                    Phone = PhoneNumber,
                    Address = Address,
                    PhotoUrl = AvatarUrl,
                    Followers = new(),
                    Following = new(),
                    Location = null
                };

                var kq = await _userService.CreateUserDocumentAsync(uid, userModel.PhotoUrl, userModel.Name, userModel.Email, userModel.Phone, userModel.Address);

                await Application.Current.MainPage.DisplayAlert("Hoàn tất", kq, "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", ex.Message, "OK");
                return;
            }   
            
           // await Shell.Current.GoToAsync("//OnboardingPage");
        }
    }
}
