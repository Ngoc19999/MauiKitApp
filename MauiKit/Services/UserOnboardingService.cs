using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiKit.Services
{
    public class UserOnboardingService
    {
        public async Task SetUserRoleAsync(string role)
        {
            var uid = CrossFirebaseAuth.Current.CurrentUser?.Uid;
            if (string.IsNullOrEmpty(uid)) return;

            await CrossFirebaseFirestore.Current
                .GetCollection("users")
                .GetDocument(uid)
                .UpdateDataAsync(("role", role));
        }

        public async Task<string> GetUserRoleAsync()
        {
            var uid = CrossFirebaseAuth.Current.CurrentUser?.Uid;
            var doc = await CrossFirebaseFirestore.Current
                .GetCollection("users")
                .GetDocument(uid)
                .GetDocumentSnapshotAsync<UserModel>();

            return doc.Data !=null && doc.Data.Role!=null ? doc.Data.Role : null;
        }
    }
}
