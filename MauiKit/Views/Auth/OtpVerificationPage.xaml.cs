namespace MauiKit.Views.Auth;

public partial class OtpVerificationPage : ContentPage
{
	public OtpVerificationPage()
	{
		InitializeComponent();
		this.BindingContext = new ViewModels.Auth.OtpVerificationViewModel();
    }
}