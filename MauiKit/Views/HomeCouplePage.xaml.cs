namespace MauiKit.Views;

public partial class HomeCouplePage : ContentPage
{
	public HomeCouplePage()
	{
		InitializeComponent();
		this.BindingContext = new HomeCoupleViewModel();

    }
}