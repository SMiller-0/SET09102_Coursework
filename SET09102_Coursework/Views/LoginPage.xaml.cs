using SET09102_Coursework.ViewModels; 

namespace SET09102_Coursework.Views;

public partial class LoginPage : ContentPage
{
	    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}