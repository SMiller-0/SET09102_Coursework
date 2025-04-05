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
            new User { Id = 1, FirstName = "Alice", Surname = "Smith", Email = "alice@example.com", Role = UserRole.Administrator },
            new User { Id = 2, FirstName = "Bob", Surname = "Brown", Email = "bob@example.com", Role = UserRole.EnvironmentalScientist }
        };
    }

    [RelayCommand]
    private async Task GoToUserDetails(User user)
    {
        await Shell.Current.GoToAsync(nameof(UserPage), true, new Dictionary<string, object>
        {
            { "SelectedUser", user }
        });
    }

    [RelayCommand]
    private void AddUser()
    {
        Users.Add(new User { Id = Users.Count + 1, FirstName = "New", Surname = "User", Email = "new@example.com", Role = UserRole.OperationsManager });
    }
}