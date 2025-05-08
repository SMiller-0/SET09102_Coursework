using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class SensorStatusPage : ContentPage
{
    public SensorStatusPage(SensorStatusViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
