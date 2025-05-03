using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class SensorReportPage : ContentPage
{
    private readonly SensorReportViewModel _viewModel;

    public SensorReportPage(SensorReportViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadSensorsCommand.Execute(null);
    }
}