using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;

namespace SET09102_Coursework.ViewModels;

public partial class AllUsersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<User> users;

    public AllUsersViewModel()
    {
        Users = new ObservableCollection<User>
        {
        };
    }

    [RelayCommand]
    private async Task ViewUserDetails(User user)
    {
        await Shell.Current.GoToAsync(nameof(UserPage), true, new Dictionary<string, object>
        {
            { "SelectedUser", user }
        });
    }

}