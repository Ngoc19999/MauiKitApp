
using MauiKit.Services;
using MauiMaps.Services;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;
using System.Diagnostics;

namespace MauiKit.ViewModels
{
    public partial class LocationMapViewModel : ObservableObject, IQueryAttributable
    {
        private readonly FirestoreUserService _userService;
        private readonly IFirebaseFirestore _firestore;
        private readonly IFirebaseAuth _auth;

        private readonly Dictionary<string, IDisposable> _listeners = new();

        [ObservableProperty]
        private ObservableCollection<CustomPin> userMarkers = new();

        [ObservableProperty]
        private string targetUserId;

        public LocationMapViewModel()
        {
            _userService = new FirestoreUserService();
            _firestore = CrossFirebaseFirestore.Current;
            _auth = CrossFirebaseAuth.Current;

            StartTrackingFollowingUsersAsync();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("uid", out var uid))
            {
                TargetUserId = uid.ToString();
            }
        }
        public event Action<Location>? RequestMapCenter;

        private void RaiseMapMove(double lat, double lng)
        {
            RequestMapCenter?.Invoke(new Location(lat, lng));
        }
        private async Task StartTrackingFollowingUsersAsync()
        {
            var currentUserId = _auth.CurrentUser?.Uid;
            if (string.IsNullOrEmpty(currentUserId)) return;

            var user = await _userService.GetUserByIdAsync(currentUserId);
            var followingList = user?.Following ?? new List<string>();

            foreach (var followedUserId in followingList)
            {
                if (_listeners.ContainsKey(followedUserId))
                    continue;

                var listener = _firestore.GetCollection("users")
                    .GetDocument(followedUserId)
                    .AddSnapshotListener<UserModel>(snapshot =>
                    {
                        if (snapshot.Data != null && snapshot.Data.Location != null)
                        {
                            var loc = snapshot.Data.Location;
                            UpdateOrAddMarker(followedUserId, snapshot.Data.Name, snapshot.Data.PhotoUrl, loc.Latitude, loc.Longitude);
                        }
                    });

                _listeners[followedUserId] = listener;
            }
        }
        private void UpdateOrAddMarker(string userId, string name, string photoUrl, double lat, double lng)
        {
            var existing = UserMarkers.FirstOrDefault(m => m.UserId == userId);
            if (existing != null)
            {
                existing.Location = new Location
                {
                    Latitude = lat,
                    Longitude = lng
                };
                existing.Label = name??"";
                existing.ImageSource = photoUrl;
            }
            else
            {
                UserMarkers.Add(new CustomPin
                {
                    UserId = userId,
                    Location = new Location
                    {
                        Latitude = lat,
                        Longitude = lng
                    },  
                    Label = name??"",
                    ImageSource = photoUrl
                });
            }

            // Notify collection changed (if needed)
            OnPropertyChanged(nameof(UserMarkers));
        }

        public void StopAllListeners()
        {
            foreach (var l in _listeners.Values)
            {
                l.Dispose();
            }

            _listeners.Clear();
        }
    }

}
