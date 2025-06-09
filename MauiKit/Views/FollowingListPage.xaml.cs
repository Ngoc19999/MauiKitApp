namespace MauiKit.Views;

public partial class FollowingListPage : ContentPage
{
	public FollowingListPage()
	{
		InitializeComponent();
		this.BindingContext = new ViewModels.FollowingListViewModel();
    }
}