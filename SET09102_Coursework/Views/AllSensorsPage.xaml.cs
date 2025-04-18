using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AllSensorsPage : ContentPage
{
    public AllSensorsPage(AllSensorsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
