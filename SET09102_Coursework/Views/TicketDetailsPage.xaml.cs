using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class TicketDetailsPage : ContentPage
{
	public TicketDetailsPage(TicketDetailsViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}