using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AllUsersPage : ContentPage
{
	public AllUsersPage(AllUsersViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}