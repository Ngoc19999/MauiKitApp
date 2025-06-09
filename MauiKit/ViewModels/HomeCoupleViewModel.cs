using MauiKit.Services;
namespace MauiKit.ViewModels
{
    public partial class HomeCoupleViewModel : ObservableObject
    {
        private readonly FirestoreUserService _userService;

        [ObservableProperty]
        private UserModel lover;

        public HomeCoupleViewModel()
        {
            _userService = new FirestoreUserService();
            LoadLoverAsync();
        }

        private async Task LoadLoverAsync()
        {
            Lover = await _userService.GetFirstFollowingUserAsync();
        }

        [RelayCommand]
        private async Task ShowMapAsync()
        {
            if (Lover != null)
                await Shell.Current.GoToAsync($"LocationMapPage?uid={Lover.Id}");
        }
    }
}
