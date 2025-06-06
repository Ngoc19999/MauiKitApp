using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;
namespace MauiKit.Services
{
    public class FirestoreUserService
    {
        private readonly IFirebaseAuth _auth;
        private readonly IFirebaseFirestore _firestore;

        public FirestoreUserService()
        {
            _auth = CrossFirebaseAuth.Current;
            _firestore = CrossFirebaseFirestore.Current;
        }

        public async Task CreateUserDocumentAsync(string uid,string PhotoUrl, string name, string email,string Phone)
        {
            var user = new UserModel
            {
                Name = name,
                Email = email,
                Phone = Phone,
                Followers = new(),
                Following = new(),
                PhotoUrl = PhotoUrl, // hoặc avatar mặc định
                Location = null,
                Role = null
            };

            await CrossFirebaseFirestore.Current
                .GetCollection("users")
                .GetDocument(uid)
                .GetDocumentSnapshotAsync<UserModel>();
        }
        public async Task<List<UserModel>> GetFollowingUsersAsync()
        {
            var currentUserId = _auth.CurrentUser?.Uid;
            var userSnapshot = await _firestore.GetCollection("users").GetDocument(currentUserId).GetDocumentSnapshotAsync<UserModel>();
            var followingIds = userSnapshot.Data.Following;

            var users = new List<UserModel>();
            foreach (var id in followingIds)
            {
                var doc = await _firestore.GetCollection("users").GetDocument(id).GetDocumentSnapshotAsync<UserModel>();
                if (doc.Data!=null)
                {
                    var user = doc.Data;
                    users.Add(user);
                }
            }

            return users;
        }
        public async Task<bool> UserExistsAsync(string uid)
        {
            var doc = await CrossFirebaseFirestore.Current
                .GetCollection("users")
                .GetDocument(uid)
                .GetDocumentSnapshotAsync<UserModel>();

            return doc.Data !=null;
        }
        public async Task<UserModel> GetUserByIdAsync(string userId)
        {
            var doc = await _firestore.GetCollection("users").GetDocument(userId).GetDocumentSnapshotAsync<UserModel>();
            return doc.Data;
        }

        public async Task<UserModel> GetFirstFollowingUserAsync()
        {
            var list = await GetFollowingUsersAsync();
            return list.FirstOrDefault();
        }
    }
}
