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

public partial class CreateUserViewModel: ObservableObject
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    [ObservableProperty] private string firstName;
    [ObservableProperty] private string middleName;
    [ObservableProperty] private string surname;
    [ObservableProperty] private string email;
    [ObservableProperty] private string phoneNumber;
    [ObservableProperty] private string street;
    [ObservableProperty] private string city;
    [ObservableProperty] private string postcode;

    [ObservableProperty] private string password;
    [ObservableProperty] private string confirmPassword;

    [ObservableProperty] private List<Role> availableRoles;
    [ObservableProperty] private Role selectedRole;

    public bool IsAdmin => _currentUserService.IsAdmin;

    public CreateUserViewModel(AppDbContext context,
                               ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
        AvailableRoles = _context.Roles.ToList();
    }

    [RelayCommand]
    private async Task Create()
    {
        // 1. required fields
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

        // 2. email format
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(Email, emailPattern))
        {
            await Shell.Current.DisplayAlert(
                "Invalid Email", "Please enter a valid email address.","OK");
            return;
        }

        // 3. postcode format 
        var postcodePattern = @"^[A-Z0-9 ]{3,20}$";
        if (!Regex.IsMatch(Postcode.ToUpper(), postcodePattern))
        {
            await Shell.Current.DisplayAlert(
                "Invalid Postcode","Please enter a valid postcode.","OK");
            return;
        }

        // 4. optional phone number (digits, +, spaces, dashes)
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

        // hash the password
        var hashed = BCrypt.Net.BCrypt.HashPassword(Password);

        var user = new User
        {
            FirstName   = FirstName,
            MiddleName  = String.IsNullOrWhiteSpace(MiddleName) ? null : MiddleName,
            Surname     = Surname,
            Email       = Email,
            PhoneNumber = String.IsNullOrWhiteSpace(MiddleName) ? null : PhoneNumber,
            Street      = Street,
            City        = City,
            Postcode    = Postcode,
            Password    = hashed,
            Role        = SelectedRole
        };

        _context.Users.Add(user);

        try
        {
            // save to DB
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

        // any other DB problem
        await Shell.Current.DisplayAlert(
            "Database Error",
            "Unable to save user right now. Please try again later.",
            "OK");
        return;
    }
    catch (Exception ex)
    {
        // unexpected
        await Shell.Current.DisplayAlert(
            "Unexpected Error",
            $"Something went wrong:\n{ex.Message}",
            "OK");
        return;
    }

    // only reach here if save succeeded
    await Shell.Current.DisplayAlert("Success",
    "New user created.", "OK");

    // navigate back to AllUsersPage and refresh list
    await Shell.Current.GoToAsync($"///AllUsersPage?created={user.Id}");
    }


    [RelayCommand]
    private async Task Cancel()
    {
        // simply go back to the list
        await Shell.Current.GoToAsync("//AllUsersPage");
    }
}