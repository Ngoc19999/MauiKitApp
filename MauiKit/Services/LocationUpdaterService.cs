using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;
using System.Diagnostics;
namespace MauiKit.Services
{
    public class LocationUpdaterService
    {

        private readonly IFirebaseFirestore _firestore;
        private readonly IFirebaseAuth _auth;
        public LocationUpdaterService()
        {
            _firestore = CrossFirebaseFirestore.Current;
            _auth = CrossFirebaseAuth.Current;
        }
        public async Task UpdateLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync()
                               ?? await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High));

                if (location == null) return;

                var userId = _auth.CurrentUser?.Uid;
                if (string.IsNullOrEmpty(userId)) return;

                var loc = new LocationModel
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Timestamp = System.DateTime.UtcNow
                };

                await _firestore.GetCollection("users").GetDocument(userId)
                      .UpdateDataAsync(("location", loc));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating location: {ex.Message}");
                // Handle exceptions, such as permission issues or location service errors
                // Log or ignore
            }
        }
    }
}
