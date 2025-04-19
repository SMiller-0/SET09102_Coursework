using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class UpdateFirmwarePage : ContentPage
{
    public UpdateFirmwarePage(UpdateFirmwareViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}