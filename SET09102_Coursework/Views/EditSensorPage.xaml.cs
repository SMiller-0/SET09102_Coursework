namespace SET09102_Coursework.Views;
using SET09102_Coursework.ViewModels;


public partial class EditSensorPage : ContentPage
{
	public EditSensorPage(EditSensorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}