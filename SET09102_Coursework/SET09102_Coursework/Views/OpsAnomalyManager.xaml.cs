using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class OpsAnomalyManager : ContentPage
{
	public OpsAnomalyManager(OpsAnomalyManagerViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}