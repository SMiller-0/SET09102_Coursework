using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using SET09102_Coursework.Services;
using SET09102_Coursework.Views;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying, editing, and managing a user's profile information.  
/// Supports edit, delete, and role-based UI control.
/// </summary>
public partial class UserViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] 
    private User user; 

    /// <summary>List of roles available for selection (used in edit mode).</summary>
    [ObservableProperty] 
    private List<Role> availableRoles;
    
    /// <summary>The currently selected role for the user.</summary>
    [ObservableProperty] 
    private Role selectedRole;
     
    [ObservableProperty, NotifyPropertyChangedFor(nameof(FullName))] private string firstName;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(FullName))] private string middleName;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(FullName))] private string surname;
   
    [ObservableProperty] 
    private string email; 

    [ObservableProperty] 
    private string phoneNumber;

    [ObservableProperty] 
    private string street;

    [ObservableProperty] 
    private string city;

    [ObservableProperty] 
    private string postcode;

    /// <summary>
    /// Indicates whether the currently logged-in user has admin privileges.
    /// </summary>
    public bool IsAdmin => _currentUserService.IsAdmin;

    /// <summary>
    /// Concatenated full name of the user (auto-updates when name fields change).
    /// </summary>
    public string FullName =>
        string.IsNullOrWhiteSpace(MiddleName) ? $"{FirstName} {Surname}" : $"{FirstName} {MiddleName} {Surname}";

    /// <summary>
    /// User's unique database ID.
    /// </summary>
    public int Id => User.Id;

    /// <summary>
    /// The user's role name (or "Unknown" if null).
    /// </summary>
    public string RoleName => User?.Role?.RoleName ?? "Unknown";
    
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    
    /// <summary>
    /// Initialises a new <see cref="UserViewModel"/> for a specific user.
    /// </summary>
    public UserViewModel(AppDbContext context, User user, ICurrentUserService currentUserService)
    {
        _context = context;
        User = user;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;

        LoadUserValues();
    }

    /// <summary>
    /// Updates the <see cref="IsAdmin"/> flag when the current user changes.
    /// </summary>
    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin)); 
    }

    /// <summary>
    /// Loads the selected user's details from the query string when navigating to this view.
    /// </summary>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            User = _context.Users.Include(u => u.Role).Single(u => u.Id == int.Parse(query["load"].ToString()));
            AvailableRoles = _context.Roles.ToList();
            LoadUserValues();
        }
    }

    /// <summary>
    /// Navigates to the user edit page if the current user is an admin.
    /// </summary>
    [RelayCommand]
    private async Task Edit()
    {
        if (!IsAdmin)
        {
            await Shell.Current.DisplayAlert("Access Denied", "You don’t have permission to perform this action.", "OK");
            return;
        }

        await Shell.Current.Navigation.PushAsync(new EditUserPage(this));

    }

    /// <summary>
    /// Saves changes made to the user after validation and updates the database.
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        try
        {
            if (!IsAdmin)
            {
                await Shell.Current.DisplayAlert("Access Denied", "You don’t have permission to perform this action.", "OK");
                return;
            }

            if (SelectedRole == null)
            {
                await Shell.Current.DisplayAlert("Missing Role", "Please select a role before saving.", "OK");
                return;
            }

            User.FirstName = FirstName;
            User.MiddleName = String.IsNullOrWhiteSpace(MiddleName) ? null : MiddleName;
            User.Surname = Surname;
            User.Email = Email;
            User.PhoneNumber = String.IsNullOrWhiteSpace(PhoneNumber) ? null : PhoneNumber;
            User.Street = Street;
            User.City = City;
            User.Postcode = Postcode;
            User.Role = SelectedRole;

            _context.SaveChanges();

            Reload();

            await Shell.Current.DisplayAlert("Saved", "User details updated.", "OK");
            await Shell.Current.GoToAsync("///AllUsersPage?saved=" + User.Id);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Something went wrong:\n{ex.Message}", "OK");
        }
    }

    /// <summary>
    /// Refreshes user data from the database and reinitializes bound properties.
    /// </summary>
    [RelayCommand]
    public void Reload()
    {
        User = _context.Users.Include(u => u.Role).Single(u => u.Id == User.Id);
        AvailableRoles = _context.Roles.ToList();
        LoadUserValues();
    }

    /// <summary>
    /// Deletes the current user after confirmation. Navigates back on success.
    /// </summary>
    [RelayCommand]
    private async Task Delete()
    {
        if (!IsAdmin)
        {
            await Shell.Current.DisplayAlert("Access Denied", "You don’t have permission to perform this action.", "OK");
            return;
        }

        var confirm = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this user?", "Yes", "Cancel");
        if (!confirm) return;
        
        try
        {
            _context.Remove(User);
            await _context.SaveChangesAsync();
            await Shell.Current.DisplayAlert("Deleted", "User deleted successfully.", "OK");
            await Shell.Current.GoToAsync("//AllUsersPage?deleted=true");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }    
    }

    /// <summary>
    /// Populates bound properties from the <see cref="User"/> model.
    /// </summary>
    public void LoadUserValues()
    {
        FirstName = User.FirstName;
        MiddleName = User.MiddleName;
        Surname = User.Surname;
        Email = User.Email;
        PhoneNumber = User.PhoneNumber;
        Street = User.Street;
        City = User.City;
        Postcode = User.Postcode;
        SelectedRole = User.Role;
        AvailableRoles = _context.Roles.ToList();
    }

    /// <summary>
    /// Cancels editing and returns to the All Users page.
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
     await Shell.Current.GoToAsync("//AllUsersPage");
    }
}
