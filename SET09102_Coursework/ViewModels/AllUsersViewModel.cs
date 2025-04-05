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
            new User { Id = 1, FirstName = "Daryl", Surname = "Smith", Email = "daryl@example.com", Role = UserRole.Administrator },
            new User { Id = 2, FirstName = "Bob", Surname = "Brown", Email = "bob@example.com", Role = UserRole.EnvironmentalScientist },
            new User { Id = 3, FirstName = "Samantha", Surname = "Jane", Email = "samanatha@example.com", Role = UserRole.EnvironmentalScientist },
            new User { Id = 4, FirstName = "Jonny", Surname = "Smith", Email = "jonny@example.com", Role = UserRole.OperationsManager },
            new User { Id = 5, FirstName = "Heather", Surname = "McKenzie", Email = "heather@example.com", Role = UserRole.OperationsManager }

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

}