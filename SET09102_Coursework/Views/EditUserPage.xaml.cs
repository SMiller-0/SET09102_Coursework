using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class EditUserPage : ContentPage
{
    public EditUserPage(UserViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
