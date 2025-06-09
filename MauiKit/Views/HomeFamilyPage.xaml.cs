namespace MauiKit.Views;

public partial class HomeFamilyPage : ContentPage
{
	public HomeFamilyPage()
	{
		InitializeComponent();
        this.BindingContext = new HomeFamilyViewModel();
    }
}