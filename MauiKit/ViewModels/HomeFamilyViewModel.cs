
using MauiKit.Services;

namespace MauiKit.ViewModels
{
    public partial class HomeFamilyViewModel : ObservableObject
    {
        private readonly FirestoreUserService _userService;

        [ObservableProperty]
        private ObservableCollection<UserModel> followingList = new();

        public HomeFamilyViewModel()
        {
            _userService = new FirestoreUserService();
            LoadFollowingAsync();
        }

        private async Task LoadFollowingAsync()
        {
            var list = await _userService.GetFollowingUsersAsync();
            FollowingList = new ObservableCollection<UserModel>(list);
        }

        [RelayCommand]
        private async Task ShowMapAsync(string userId)
        {
            await Shell.Current.GoToAsync($"LocationMapPage?uid={userId}");
        }
    }
}
