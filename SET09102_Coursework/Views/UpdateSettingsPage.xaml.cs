using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class UpdateSettingsPage : ContentPage
{
    public UpdateSettingsPage(UpdateSettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
