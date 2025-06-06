using MauiKit.Services;
using Plugin.Firebase.Auth;


namespace MauiKit.ViewModels.Auth
{
    public partial class OtpVerificationViewModel : ObservableObject
    {
        private readonly IFirebaseAuth _auth;
        private readonly FirestoreUserService _firestoreService;

        [ObservableProperty]
        private string otpCode;
        [ObservableProperty]
        private string phone;

        public OtpVerificationViewModel()
        {
            _auth = CrossFirebaseAuth.Current;
            _firestoreService = new FirestoreUserService();
        }

        [RelayCommand]
        private async Task VerifyOtpAsync()
        {
            try
            {
                var result = await _auth.SignInWithPhoneNumberVerificationCodeAsync(OtpCode);
                var uid = result.Uid;

                // Tạo user document Firestore nếu chưa có
                if (!await _firestoreService.UserExistsAsync(uid))
                {
                    await _firestoreService.CreateUserDocumentAsync(uid, result.PhotoUrl, result.DisplayName, result.Email, result.ProviderInfos.FirstOrDefault()?.PhoneNumber?? phone);
                }

                await Shell.Current.GoToAsync("//OnboardingPage");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Xác minh thất bại", ex.Message, "OK");
            }
        }
    }
}
