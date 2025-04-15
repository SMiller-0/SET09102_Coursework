using SET09102_Coursework.ViewModels;
namespace SET09102_Coursework.Views;

public partial class AdminBackUp : ContentPage
{
    public AdminBackUp(AdminBackUpViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}