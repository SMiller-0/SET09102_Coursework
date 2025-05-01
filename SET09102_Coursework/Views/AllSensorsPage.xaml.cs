using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AllSensorsPage : ContentPage
{
    private readonly AllSensorsViewModel _viewModel;

    public AllSensorsPage(AllSensorsViewModel viewModel)
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
