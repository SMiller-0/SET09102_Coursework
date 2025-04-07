using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AllUsersPage : ContentPage
{
	public AllUsersPage()
	{
		this.BindingContext = new AllUsersViewModel();
		InitializeComponent();
	}
}