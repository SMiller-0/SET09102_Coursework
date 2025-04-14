using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

        AllUsers = new ObservableCollection<UserViewModel>();
        RefreshUserList();

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
        if (query.ContainsKey("deleted") || query.ContainsKey("saved"))
        {
            RefreshUserList();
        }
    }
    
        
        private void RefreshUserList()
{
    AllUsers.Clear();

    var sortedUsers = _context.Users
        .Include(u => u.Role)
        .OrderBy(u => u.Surname)
        .ThenBy(u => u.FirstName)
        .ToList();

    foreach (var user in sortedUsers)
    {
        AllUsers.Add(new UserViewModel(_context, user, _currentUserService));
    }
}

    }