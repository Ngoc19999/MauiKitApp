using MauiKit.Models;

namespace MauiKit.Services
{
    public static class UserSession
    {
        public static MauiKitUser CurrentUser { get; set; }

       
        public static LoginDto LoginDto { get; set; }
    }
}
