using MauiKit.ViewModels.DemoApp;


namespace MauiKit.Views.DemoApp;

public partial class SigninPage : ContentPage
{
    public SigninPage()
    {
        InitializeComponent();
        BindingContext = new SigninViewModel();
    }

}