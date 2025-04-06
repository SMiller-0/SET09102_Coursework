using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class SensorPage : ContentPage
{
	public SensorPage()
	{
		InitializeComponent();
		BindingContext = new SensorViewModel();

	}
}