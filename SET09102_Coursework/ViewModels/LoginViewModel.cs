using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;
using SET09102_Coursework.Data;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for handling user login and logout operations.
/// Validates credentials and manages navigation after login.
/// </summary>
public partial class LoginViewModel : ObservableObject 
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    [ObservableProperty] 
    private string email;

    [ObservableProperty] 
    private string password;

    [ObservableProperty] 
    private string loginError;

    /// <summary>
    /// Flag that indicates whether login has failed.
    // </summary>
    [ObservableProperty] private bool isLoginFailed;

    /// <summary>
    /// Constructs a new <see cref="LoginViewModel"/>.
    /// </summary>
    /// <param name="context">Database context used to fetch user credentials.</param>
    /// <param name="currentUserService">Service for tracking current logged-in user state.</param>
    public LoginViewModel(AppDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Attempts to log in the user. Validates input, checks credentials,
    /// and navigates to the dashboard on success. Otherwise, sets error state.
    /// </summary>
    [RelayCommand]
    private async Task Login()
    {
        IsLoginFailed = false;
        LoginError = string.Empty;

        var trimmedEmail = Email?.Trim();
        var trimmedPassword = Password?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedEmail) || string.IsNullOrWhiteSpace(trimmedPassword))
        {
            LoginError = "Email and password are required.";
            IsLoginFailed = true;
            return;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(trimmedEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            LoginError = "Please enter a valid email address.";
            IsLoginFailed = true;
            return;
        }

        try
        {
            var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == trimmedEmail);

            // Hardcoded password for development only (remove in production)
            bool isPasswordValid =
                 trimmedPassword == "pw" ||
                 trimmedPassword == "Password123!" ||
                 (user != null && BCrypt.Net.BCrypt.Verify(trimmedPassword, user.Password));

            if (user != null && isPasswordValid)
            {
                _currentUserService.SetUser(user);
                await Shell.Current.GoToAsync($"//DashboardPage");
            }
            else
            {
                LoginError = "Invalid email or password.";
                IsLoginFailed = true;
            }
        }
        catch (Exception ex)
        {
            LoginError = "Invalid email or password.";
            IsLoginFailed = true;
        }
    }

    /// <summary>
    /// Logs the user out and navigates back to the login page.
    /// </summary>
    [RelayCommand]
    private async Task Logout()
    {
        _currentUserService.Logout();
        await Shell.Current.DisplayAlert("Logged out", "You have been logged out.", "OK");
        Application.Current.MainPage = new AppShell();
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
