namespace MauiKit.Views;

public partial class OnboardingPage : ContentPage
{
	public OnboardingPage()
	{
		InitializeComponent();
		this.BindingContext = new OnboardingViewModel();
    }
}