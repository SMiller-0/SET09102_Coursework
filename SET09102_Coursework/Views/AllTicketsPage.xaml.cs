using SET09102_Coursework.ViewModels;
using CommunityToolkit.Mvvm.Input;  

namespace SET09102_Coursework.Views;

public partial class AllTicketsPage : ContentPage
{
	AllTicketsViewModel _vm;
	public AllTicketsPage(AllTicketsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _vm = viewModel;
	}

	protected override async void OnAppearing()
    {
        
        try
        {
            if (_vm.LoadByStatusCommand is IAsyncRelayCommand asyncCmd)
            {
                await asyncCmd.ExecuteAsync(null);
            }
            else if (_vm.LoadByStatusCommand.CanExecute(null))
            {
                _vm.LoadByStatusCommand.Execute(null);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to refresh tickets: {ex}");
        }
    }

	
}