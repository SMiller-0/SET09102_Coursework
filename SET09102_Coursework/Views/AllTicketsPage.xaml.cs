using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AllTicketsPage : ContentPage
{
	public AllTicketsPage(AllTicketsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}