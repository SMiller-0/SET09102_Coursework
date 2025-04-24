using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;
using SET09102_Coursework.Data;
using System.Windows.Input;
using System.Collections.Generic;

namespace SET09102_Coursework.ViewModels;

public class AllUsersViewModel : IQueryAttributable
{
    public ObservableCollection<UserViewModel> AllUsers { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectUserCommand { get; }
    private AppDbContext _context;
    
    public AllUsersViewModel(AppDbContext appDbContext)
    {
        _context = appDbContext;

        AllUsers = new ObservableCollection<UserViewModel>(
        _context.Users.ToList().Select(u => new UserViewModel(_context, u))
        );
        NewCommand = new AsyncRelayCommand(NewUserAsync);
        SelectUserCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
    }

    private async Task NewUserAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.UserPage));
    }


    private async Task SelectUserAsync(UserViewModel user)
    {
        if (user != null)
        {
            await Shell.Current.GoToAsync($"{nameof(Views.UserPage)}?load={user.Id}");
        }
    }


    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string userId = query["deleted"].ToString();
            var matchedUser = AllUsers.FirstOrDefault(u => u.Id == int.Parse(userId));

            if (matchedUser != null)
                AllUsers.Remove(matchedUser);
        }
        else if (query.ContainsKey("saved"))
        {
            string userId = query["saved"].ToString();
            var matchedUser = AllUsers.FirstOrDefault(u => u.Id == int.Parse(userId));

            if (matchedUser != null)
            {
                matchedUser.Reload();
                AllUsers.Move(AllUsers.IndexOf(matchedUser), 0);
            }
            else
            {
                var newUser = _context.Users.Single(u => u.Id == int.Parse(userId));
                AllUsers.Insert(0, new UserViewModel(_context, newUser));
            }
        }
    }


   

}