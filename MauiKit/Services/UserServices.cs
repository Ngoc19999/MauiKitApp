//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using System.Threading.Tasks;
//using MauiKit.Models;

//namespace MauiKit.Services
//{
//    public class SaveDataLogin
//    {
//        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.json");

//        // Đọc dữ liệu từ tệp JSON
//        public async Task<List<UserModels>> LoadUsersAsync()
//        {
//            if (!File.Exists(_filePath))
//                return new List<UserModels>();

//            var json = await File.ReadAllTextAsync(_filePath);
//            return JsonSerializer.Deserialize<List<UserModels>>(json) ?? new List<UserModels>();
//        }

//        // Ghi dữ liệu vào tệp JSON
//        public async Task<bool> SaveUsersAsync(List<UserModels> users)
//        {
//            var json = JsonSerializer.Serialize(users);
//            await File.WriteAllTextAsync(_filePath, json);
//            return true;
//        }

//        // Thêm tài khoản mới
//        public async Task<bool> AddUserAsync(UserModels newUser)
//        {
//            var users = await LoadUsersAsync();
//            if (users.Exists(u => u.user == newUser.user))
//                return false; // Tài khoản đã tồn tại

//            newUser.id = users.Count + 1;
//            users.Add(newUser);
//            await SaveUsersAsync(users);
//            return true;
//        }

//        // Cập nhật mật khẩu cho tài khoản
//        public async Task<bool> UpdatePasswordAsync(string username, string newPassword)
//        {
//            var users = await LoadUsersAsync();
//            var user = users.Find(u => u.user == username);

//            if (user != null)
//            {
//                user.passWord = newPassword;
//                await SaveUsersAsync(users);
//                return true;
//            }

//            return false; // Không tìm thấy tài khoản
//        }

//        // Xóa tài khoản theo ID
//        public async Task<bool> DeleteUserAsync(int id)
//        {
//            var users = await LoadUsersAsync();
//            var userToRemove = users.Find(u => u.id == id);

//            if (userToRemove != null)
//            {
//                users.Remove(userToRemove);
//                await SaveUsersAsync(users);
//                return true;
//            }

//            return false; // Không tìm thấy tài khoản
//        }
//    }
//}