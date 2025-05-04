using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class TrendReportPage : ContentPage
{
    public TrendReportPage(TrendReportViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}