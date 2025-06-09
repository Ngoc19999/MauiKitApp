
namespace MauiKit.Services
{
    using Plugin.Firebase.Auth;
    using Plugin.Firebase.Firestore;

    public class LocationService
    {
        private readonly IFirebaseFirestore _firestore;
        private readonly IFirebaseAuth _auth;
        public LocationService()
        {
            _firestore = CrossFirebaseFirestore.Current;
            _auth = CrossFirebaseAuth.Current;
        }

        public async Task<UserModel> GetUserAsync(string userId)
        {
            var doc = await _firestore.GetCollection("users").GetDocument(userId).GetDocumentSnapshotAsync<UserModel>();
            if (doc.Data==null) return null;
           
            return doc.Data;
        }

       

        public async Task ListenToUserLocationAsync(string userId, Action<LocationModel> onLocationUpdate)
        {
            _firestore
                .GetCollection("users")
                .GetDocument(userId)
                .AddSnapshotListener<UserModel>(async snapshot =>
                {
                    if (snapshot.Data !=null && snapshot.Data.Location !=null)
                    {
                        var loc = snapshot.Data.Location;
                        onLocationUpdate?.Invoke(loc);
                    }
                });
        }
    }

}
