using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class SensorPage : ContentPage
{
	public SensorPage(SensorViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}
