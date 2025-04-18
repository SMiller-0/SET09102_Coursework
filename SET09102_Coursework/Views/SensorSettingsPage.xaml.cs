using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class SensorSettingsPage : ContentPage
{
    public SensorSettingsPage(SensorSettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
