using MauiKit.Services;
using MauiKit.ViewModels.DemoApp;

namespace MauiKit.Views.DemoApp;

public partial class UpdateUser : ContentPage
{

    public UpdateUser()
    {
        InitializeComponent();
        BindingContext = UserSession.CurrentUser;
    }
}