using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;
using SET09102_Coursework.Data;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for handling user login logic.
/// </summary>
public partial class LoginViewModel : ObservableObject 
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string loginError;

    [ObservableProperty]
    private bool isLoginFailed;

    public LoginViewModel(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Command bound to the Login button. Attempts to validate user credentials.
    /// </summary>
    [RelayCommand]
    private async Task Login()
    {
        IsLoginFailed = false;
        LoginError = string.Empty;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            LoginError = "Email and password are required.";
            IsLoginFailed = true;
            return;
        }

        // Look up user by email
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == Email);

        // Hardcoded password for development only remove in production
        if (user != null && Password == "Password123!" || BCrypt.Net.BCrypt.Verify(Password, user.Password))
        {
            await Shell.Current.GoToAsync($"//AllUsersPage");
        }
        else
        {
            LoginError = "Invalid email or password.";
            IsLoginFailed = true;
        }
    }
}
