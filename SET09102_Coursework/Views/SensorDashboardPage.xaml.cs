using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class SensorDashboardPage : ContentPage
{
    public SensorDashboardPage(SensorDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}