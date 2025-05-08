using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for the dashboard page.  
/// Displays a dynamic welcome message based on the logged-in user.
/// </summary>
public partial class DashboardViewModel: ObservableObject
{
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initialises a new instance of the <see cref="DashboardViewModel"/>.
    /// Subscribes to user change events to update the welcome message.
    /// </summary>
    public DashboardViewModel(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;
    }

    /// <summary>
    /// A welcome message that greets the current user by first name.
    /// Falls back to a generic greeting if no user is logged in.
    /// </summary>
    public string WelcomeMessage
    {
        get
        {
            var user = _currentUserService.LoggedInUser;
            if (user == null)
            {
                return "Welcome!";
            }

            return $"Welcome, {user.FirstName}!";
        }
    }

    /// <summary>
    /// Updates the welcome message when the user context changes.
    /// </summary>
    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(WelcomeMessage));
    }
}