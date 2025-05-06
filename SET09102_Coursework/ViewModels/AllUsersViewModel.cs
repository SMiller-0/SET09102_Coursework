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

/// <summary>
/// ViewModel for displaying and managing the list of users.
/// Provides navigation to view or create users, and updates list when users change.
/// </summary>
public partial class AllUsersViewModel : ObservableObject, IQueryAttributable
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ObservableCollection<UserViewModel> AllUsers { get; }

    public ICommand SelectUserCommand { get; }

    public bool IsAdmin => _currentUserService.IsAdmin;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllUsersViewModel"/> class.
    /// Loads the user list and sets up role-change tracking.
    /// </summary>
    public AllUsersViewModel(AppDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _context = appDbContext;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;

        AllUsers = new ObservableCollection<UserViewModel>();
        RefreshUserList();

        SelectUserCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
    }

    /// <summary>
    /// Updates the <see cref="IsAdmin"/> property when the current user changes.
    /// </summary>
    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin)); 
    }

    /// <summary>
    /// Navigates to the page for creating a new user.
    /// </summary>
    [RelayCommand]
    private async Task AddUser()
    {
         await Shell.Current.GoToAsync(nameof(CreateUserPage));
    }


    /// <summary>
    /// Navigates to the selected user's detail page.
    /// </summary>
    /// <param name="user">The selected user.</param>
    private async Task SelectUserAsync(UserViewModel user)
    {
        if (user != null)
        {
            await Shell.Current.Navigation.PushAsync(new UserPage(user));        
        }
    }

    /// <summary>
    /// Called when navigation query parameters indicate a user was created, edited, or deleted.
    /// Refreshes the user list if needed.
    /// </summary>
    /// <param name="query">The navigation query parameters.</param>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted") || query.ContainsKey("saved") || query.ContainsKey("created"))
        {
            RefreshUserList();
        }
    }
    
    /// <summary>
    /// Retrieves users from the database and populates the <see cref="AllUsers"/> collection.
    /// Users are sorted by surname, then first name.
    /// </summary>
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