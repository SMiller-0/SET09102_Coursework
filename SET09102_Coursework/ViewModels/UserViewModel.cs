using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
namespace SET09102_Coursework.ViewModels;


[QueryProperty(nameof(SelectedUser), "SelectedUser")]
public partial class UserViewModel : ObservableObject
{
    [ObservableProperty]
    private User selectedUser;

}
