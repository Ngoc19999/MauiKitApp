using Plugin.Firebase.Auth;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Firestore;
using System.Diagnostics;
namespace MauiKit.Services
{
    public class FirebaseNotificationService
    {
        private readonly IFirebaseAuth _auth;
        private readonly IFirebaseCloudMessaging _messaging;
        private readonly IFirebaseFirestore _firestore;

        public FirebaseNotificationService()
        {
            _auth = CrossFirebaseAuth.Current;
            _messaging = CrossFirebaseCloudMessaging.Current;
            _firestore = CrossFirebaseFirestore.Current;
        }

        public void Initialize()
        {
            // Đăng ký sự kiện nhận tin nhắn khi app đang mở
            _messaging.NotificationReceived += _messaging_NotificationReceived;

            // Đăng ký khi token thay đổi (user cấp quyền sau này, hoặc Firebase đổi)
            _messaging.TokenChanged += _messaging_TokenChanged;
        }

        private async void _messaging_NotificationReceived(object? sender, Plugin.Firebase.CloudMessaging.EventArgs.FCMNotificationReceivedEventArgs e)
        {
            var title = e.Notification?.Title ?? "Thông báo";
            var body = e.Notification?.Body ?? "";

            // Hiển thị thông báo đơn giản
            await Shell.Current.DisplayAlert(title, body, "OK");
        }

        private async void _messaging_TokenChanged(object? sender, Plugin.Firebase.CloudMessaging.EventArgs.FCMTokenChangedEventArgs e)
        {
            Debug.WriteLine($"[FCM] Token refreshed: {e.Token}");
            await RegisterFcmTokenAsync(); // tự động ghi mới
        }

        /// <summary>
        /// Ghi token FCM vào Firestore nếu user đã login
        /// </summary>
        public async Task RegisterFcmTokenAsync()
        {
            try
            {
                var user = _auth.CurrentUser;
                if (user == null)
                {
                    Debug.WriteLine("[FCM] User chưa đăng nhập, không thể lưu token.");
                    return;
                }

                var token = await _messaging.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    Debug.WriteLine("[FCM] Token trống.");
                    return;
                }

                var userDoc = _firestore.GetCollection("users").GetDocument(user.Uid);
                var snapshot = await userDoc.GetDocumentSnapshotAsync<UserModel>();
                var existingToken = snapshot.Data.FcmToken;

                if (existingToken != token)
                {
                    await userDoc.UpdateDataAsync(("fcmToken", token));

                    Debug.WriteLine("[FCM] Token mới đã được cập nhật vào Firestore.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FCM] Lỗi lưu token: {ex.Message}");
            }
        }
    }
}
