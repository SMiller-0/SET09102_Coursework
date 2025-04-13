using SET09102_Coursework.ViewModels; 

namespace SET09102_Coursework.Views;

public partial class LoginPage : ContentPage
{
	    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

	protected override void OnAppearing()
    {
        base.OnAppearing();

    base.OnAppearing();

    if (BindingContext is LoginViewModel vm)
		{
			vm.Email = string.Empty;
        	vm.Password = string.Empty;
        	vm.LoginError = string.Empty;
        	vm.IsLoginFailed = false;
    	}
	}
}