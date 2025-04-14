using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class UserPage : ContentPage
{
		public UserPage (UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}