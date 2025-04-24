namespace SET09102_Coursework.Views;
using SET09102_Coursework.ViewModels;
using Microsoft.Maui.Controls;

public partial class CreateTicketPage : ContentPage
{
	public CreateTicketPage(CreateTicketViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
	}
}