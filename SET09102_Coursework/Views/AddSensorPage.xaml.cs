using SET09102_Coursework.ViewModels;
using Microsoft.Maui.Controls;

namespace SET09102_Coursework.Views;

public partial class AddSensorPage : ContentPage
{
	public AddSensorPage(AddSensorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
	}
}