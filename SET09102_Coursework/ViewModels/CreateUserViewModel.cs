using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;
namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for the Create User page.  
/// Handles user input, validation, password hashing, and saving a new user.
/// </summary>
public partial class CreateUserViewModel: ObservableObject

{   
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    
    [ObservableProperty] 
    private string firstName;

    /// <summary>
    /// Middle name of the new user (optional).
    /// </summary>
    [ObservableProperty] 
    private string middleName;

    [ObservableProperty] 
    private string surname;
    
    /// <summary>
    /// The new user’s corporate email address.  
    /// This is initialised to the domain “@smartsense.com” so that
    /// the user only needs to type the local part (e.g. “j.doe”)
    /// to form “j.doe@smartsense.com”.  
    /// </summary> 
    [ObservableProperty] 
    private string email = "@smartsense.com";
    
    /// <summary>
    /// Phone number of the new user (optional).
    /// </summary>
    [ObservableProperty] 
    private string phoneNumber;
    
    [ObservableProperty] 
    private string street;
   
    [ObservableProperty] 
    private string city;
    
    [ObservableProperty] 
    private string postcode;
    
    /// <summary>
    /// Password for the new user (must match <see cref="ConfirmPassword"/>).
    /// </summary>
    [ObservableProperty] private string password;
    
    /// <summary>
    /// Confirmation of the password.
    /// </summary>
    [ObservableProperty] private string confirmPassword;
    
    /// <summary>
    // List of available roles to choose from.
    // </summary>
    [ObservableProperty] 
    private List<Role> availableRoles;

    /// <summary>
    // The role selected for the new user.
    // </summary>
    [ObservableProperty] private Role selectedRole;
    
    /// <summary>
    /// Indicates whether the current user has admin privileges.
    /// Used to enable/disable the Create operation.
    /// </summary>
    public bool IsAdmin => _currentUserService.IsAdmin;


    /// <summary>
    /// Constructs a new <see cref="CreateUserViewModel"/>.  
    /// Loads the available roles from the database.
    /// </summary>
    /// <param name="context">Database context for accessing user data <see cref="AppDbContext"/>.</param>
    /// <param name="currentUserService">Service that provides info about the currently logged‑in user (for IsAdmin).
    /// </param>
    public CreateUserViewModel(AppDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
        AvailableRoles = _context.Roles.ToList();
    }


   /// <summary>
    /// Command to create a new user.  
    /// Validates inputs, hashes the password, writes to the database,
    /// and navigates back to the all‑users page on success.
    /// </summary>
    /// <returns>A <see cref="Task"/> that completes when the creation flow finishes.</returns>
    /// <remarks>
    /// This method performs several validation checks on the user input:
    /// 1) Required fields and matching password.
    /// 2) Email format.
    /// 3) Postcode format.
    /// 4) Optional phone number format.
    /// 5) Password strength: minimum 8 characters, at least one uppercase letter, one lowercase letter, and one digit.
    /// </remarks>
    [RelayCommand]
    private async Task Create()
    {
        // Validation checks:
        if (string.IsNullOrWhiteSpace(FirstName)
        || string.IsNullOrWhiteSpace(Surname)
        || string.IsNullOrWhiteSpace(Email)
        || string.IsNullOrWhiteSpace(Password)
        || Password != ConfirmPassword
        || SelectedRole == null)
        {
            await Shell.Current.DisplayAlert("Missing Info",
                "Please fill out all fields and ensure password matches.","OK");
            return;
        }

        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(Email, emailPattern))
        {
            await Shell.Current.DisplayAlert(
                "Invalid Email", "Please enter a valid email address.","OK");
            return;
        }

        var postcodePattern = @"^[A-Z0-9 ]{3,20}$";
        if (!Regex.IsMatch(Postcode.ToUpper(), postcodePattern))
        {
            await Shell.Current.DisplayAlert(
                "Invalid Postcode","Please enter a valid postcode.","OK");
            return;
        }

        if (!string.IsNullOrWhiteSpace(PhoneNumber))
        {
            var phonePattern = @"^[\d\+\-\s]{5,20}$";
            if (!Regex.IsMatch(PhoneNumber, phonePattern))
            {
                await Shell.Current.DisplayAlert(
                    "Invalid Phone","Please enter a valid phone number.","OK");
                return;
            }
        }

        var pwdPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        if (!Regex.IsMatch(Password, pwdPattern))
        {
            await Shell.Current.DisplayAlert(
                "Weak Password",
                "Password must be at least 8 characters and include uppercase, lowercase, and a number.",
                "OK");
            return;
        }

        // Hash password with BCrypt and create user entity
        var hashed = BCrypt.Net.BCrypt.HashPassword(Password);

        var user = new User
        {
            FirstName   = FirstName,
            MiddleName  = String.IsNullOrWhiteSpace(MiddleName) ? null : MiddleName,
            Surname     = Surname,
            Email       = Email,
            PhoneNumber = String.IsNullOrWhiteSpace(phoneNumber) ? null : PhoneNumber,
            Street      = Street,
            City        = City,
            Postcode    = Postcode,
            Password    = hashed,
            Role        = SelectedRole
        };

        _context.Users.Add(user);

        try
        {
            // Attempt to save to DB
             await _context.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            // handle duplicate‐email (UNIQUE index) errors
            if (dbEx.InnerException?.Message.Contains("IX_Users_Email") ?? false)
            {
                await Shell.Current.DisplayAlert(
                "Duplicate Email",
                "That email is already in use. Please choose another.",
                "OK");
            return;
            }

             // Any other database error
            await Shell.Current.DisplayAlert(
                "Database Error",
                "Unable to save user right now. Please try again later.",
                "OK");
            return;
        }
        catch (Exception ex)
        {
            // Any other error
            await Shell.Current.DisplayAlert(
                "Unexpected Error",
                $"Something went wrong:\n{ex.Message}",
                "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Success", "New user created.", "OK");
        await Shell.Current.GoToAsync($"///AllUsersPage?created={user.Id}");
    }

    /// <summary>
    /// Cancels user creation and navigates back to the All Users page.
    /// </summary>
    /// <returns>A <see cref="Task"/> that completes when navigation finishes.</returns>
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("//AllUsersPage");
    }
}