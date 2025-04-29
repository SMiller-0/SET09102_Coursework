using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AnalyticalTestsEnviromental : ContentPage
{
    public AnalyticalTestsEnviromental(AnalyticalTestsEnviromentalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}