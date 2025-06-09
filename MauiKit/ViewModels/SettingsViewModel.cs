using MauiKit.Views;
using MauiKit.Views.Auth;
using Plugin.Firebase.Auth;

namespace MauiKit.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IFirebaseAuth _auth;
        [ObservableProperty] private string displayName;
        [ObservableProperty] private string phoneNumber;
        [ObservableProperty] private string avatarUrl;
        [ObservableProperty] private string userID;
        public SettingsViewModel()
        {
            _auth = CrossFirebaseAuth.Current;
            UserID = _auth.CurrentUser?.Uid ?? string.Empty;
        }

        [RelayCommand]
        private async Task NavigateToUpdateProfileAsync()
        {
            await Shell.Current.Navigation.PushAsync(new RegisterProfilePage());
            // await Shell.Current.GoToAsync("//UpdateProfilePage");
        }

        [RelayCommand]
        private async Task NavigateToOnboardingAsync()
        {
            await Shell.Current.Navigation.PushAsync(new OnboardingPage());
            // await Shell.Current.GoToAsync("//OnboardingPage");
        }

        [RelayCommand]
        private async Task NavigateToFollowingListAsync()
        {
            await Shell.Current.Navigation.PushAsync(new FollowingListPage());
            // await Shell.Current.GoToAsync("//FollowingListPage");
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            await _auth.SignOutAsync();
            await Shell.Current.GoToAsync("//PhoneLoginPage");
        }   

    }
}
