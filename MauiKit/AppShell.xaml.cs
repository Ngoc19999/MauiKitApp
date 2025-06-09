using MauiKit.Services;
using static Microsoft.Maui.ApplicationModel.Permissions;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace MauiKit;

public partial class AppShell : Shell
{

    private readonly FirestoreUserService _firestoreService;
    public AppShell()
    {
        InitializeComponent();
        _firestoreService = new FirestoreUserService();
    }

    public async Task NavigateAfterLoginAsync(string uid)
    {
        if (!await _firestoreService.UserExistsAsync(uid))
        {
            await Shell.Current.GoToAsync("//RegisterProfilePage");
        }
        else
        {
           // await Shell.Current.GoToAsync("//OnboardingPage");
            await Shell.Current.GoToAsync("//LocationMapPage?uid=GQ65llCebufkxOMbG9FXS6WTCHC3");
        }
            
        //var user = await userService.GetCurrentUserAsync();

        //if (string.IsNullOrEmpty(user.Role))
        //{
        //    // Chưa chọn vai trò → chuyển đến onboarding
        //    await Shell.Current.GoToAsync("//OnboardingPage");
        //}
        //else
        //{
        //    switch (user.Role)
        //    {
        //        case "family":
        //            await Shell.Current.GoToAsync("//HomeFamilyPage");
        //            break;
        //        case "couple":
        //            await Shell.Current.GoToAsync("//HomeCouplePage");
        //            break;
        //        case "business":
        //            await Shell.Current.GoToAsync("//HomeBusinessPage");
        //            break;
        //    }
        //}
    }
}