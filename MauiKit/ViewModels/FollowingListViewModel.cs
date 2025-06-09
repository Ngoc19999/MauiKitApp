using MauiKit.Services;
using Plugin.Firebase.Auth;

namespace MauiKit.ViewModels
{
    public partial class FollowingListViewModel : ObservableObject
    {
        private readonly FirestoreUserService _userService;
        private readonly IFirebaseAuth _auth;

        [ObservableProperty] private string newUserId;
        [ObservableProperty] private ObservableCollection<UserModel> followingList = new();

        public FollowingListViewModel()
        {
            _auth = CrossFirebaseAuth.Current;
            _userService = new FirestoreUserService();
            LoadFollowingAsync();
        }

        private async void LoadFollowingAsync()
        {
            var currentUser = await _userService.GetUserByIdAsync(_auth.CurrentUser.Uid);
            if (currentUser?.Following == null) return;

            FollowingList.Clear();

            foreach (var uid in currentUser.Following)
            {
                var user = await _userService.GetUserByIdAsync(uid);
                if (user != null)
                    FollowingList.Add(user);
            }
        }

        [RelayCommand]
        private async Task AddFollowingAsync()
        {
            if (string.IsNullOrWhiteSpace(NewUserId)) return;

            var myUid = _auth.CurrentUser.Uid;
            var success = await _userService.AddFollowingAsync(myUid, NewUserId);

            if (success)
            {
                var user = await _userService.GetUserByIdAsync(NewUserId);
                if (user != null) FollowingList.Add(user);
                NewUserId = string.Empty;
            }
        }

        [RelayCommand]
        private async Task RemoveFollowingAsync(string userIdToRemove)
        {
            var myUid = _auth.CurrentUser.Uid;

            await _userService.RemoveFollowingAsync(myUid, userIdToRemove);
            var user = FollowingList.FirstOrDefault(u => u.Id == userIdToRemove);
            if (user != null) FollowingList.Remove(user);
        }
    }
}
