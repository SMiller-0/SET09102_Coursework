using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;
public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}