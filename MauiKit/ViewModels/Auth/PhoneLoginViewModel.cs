using MauiKit.Views.Auth;
using Plugin.Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiKit.ViewModels.Auth
{
    public partial class PhoneLoginViewModel : ObservableObject
    {
        private readonly IFirebaseAuth _auth;

        [ObservableProperty]
        private string phoneNumber;

        public PhoneLoginViewModel()
        {
            _auth = CrossFirebaseAuth.Current;
        }

        [RelayCommand]
        private async Task SendOtpAsync()
        {
            try
            {
                await _auth.VerifyPhoneNumberAsync(PhoneNumber);
                await Shell.Current.Navigation.PushAsync(new OtpVerificationPage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", ex.Message, "OK");
            }
        }
    }
}
