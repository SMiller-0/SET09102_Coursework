using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;
using SET09102_Coursework.Data;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public class AllUsersViewModel : ObservableObject, IQueryAttributable
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ObservableCollection<UserViewModel> AllUsers { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectUserCommand { get; }

    public bool IsAdmin => _currentUserService.IsAdmin;


    public AllUsersViewModel(AppDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _context = appDbContext;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;

        AllUsers = new ObservableCollection<UserViewModel>(
        _context.Users.ToList().Select(u => new UserViewModel(_context, u, _currentUserService))
        );

        NewCommand = new AsyncRelayCommand(NewUserAsync);
        SelectUserCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
    }

    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin)); 
    }


    private async Task NewUserAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.UserPage));
    }


    private async Task SelectUserAsync(UserViewModel user)
    {
        if (user != null)
        {
            await Shell.Current.Navigation.PushAsync(new UserPage(user));        
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
                AllUsers.Insert(0, new UserViewModel(_context, newUser, _currentUserService));
            }
        }
    }
}