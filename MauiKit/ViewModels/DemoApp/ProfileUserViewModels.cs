
using MauiKit.Services;

namespace MauiKit.ViewModels.DemoApp
{
    public class ProfileUserViewModels : INotifyPropertyChanged
    {
        private MauiKitUser _userProfile;

        public MauiKitUser UserProfile
        {
            get => _userProfile;
            set
            {
                _userProfile = value;
                OnPropertyChanged();
            }
        }

        private readonly ApiServiceBase _apiService = new ApiServiceBase();

        public async Task LoadUserDataAsync(LoginDto loginDto)
        {
            var response = await _apiService.LoginUserAsync(loginDto);
            if (response != null && response.IsSuccessful)
            {
                UserProfile = response.Data;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
