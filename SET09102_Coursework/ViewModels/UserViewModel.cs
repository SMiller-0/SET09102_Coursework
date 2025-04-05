using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using System.Diagnostics;
namespace SET09102_Coursework.ViewModels;


[QueryProperty(nameof(SelectedUser), "SelectedUser")]
public partial class UserViewModel : ObservableObject
{
    [ObservableProperty]
    private User selectedUser;

    [RelayCommand]
    private void EditUser()
    {
        Debug.WriteLine($"Editing user: {SelectedUser.FirstName} {SelectedUser.Surname}");
    }

    [RelayCommand]
    private void DeleteUser()
    {
        Debug.WriteLine($"Deleting user: {SelectedUser.FirstName} {SelectedUser.Surname}");
    }
}
