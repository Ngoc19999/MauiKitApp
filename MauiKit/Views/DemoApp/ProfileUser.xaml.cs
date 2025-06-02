using MauiKit.Services;
using MauiKit.ViewModels.DemoApp;

namespace MauiKit.Views.DemoApp;

public partial class ProfileUser : ContentPage
{
   
    public ProfileUser()
	{
		InitializeComponent();
        BindingContext = UserSession.CurrentUser;
    } 
}