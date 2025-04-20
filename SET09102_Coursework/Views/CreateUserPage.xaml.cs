
using SET09102_Coursework.ViewModels;
using Microsoft.Maui.Controls;

namespace SET09102_Coursework.Views;
 public partial class CreateUserPage : ContentPage

{
    public CreateUserPage(CreateUserViewModel viewModel)

    {
        InitializeComponent();
        BindingContext = viewModel;
	}
}