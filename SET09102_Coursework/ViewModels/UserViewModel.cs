using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using SET09102_Coursework.Services;
using SET09102_Coursework.Views;


namespace SET09102_Coursework.ViewModels;

public partial class UserViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private User user; 

    [ObservableProperty] private List<Role> availableRoles;
    [ObservableProperty] private Role selectedRole;
     
    [ObservableProperty, NotifyPropertyChangedFor(nameof(FullName))] private string firstName;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(FullName))] private string middleName;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(FullName))] private string surname;
   
    [ObservableProperty] private string email;  
    [ObservableProperty] private string phoneNumber;
    [ObservableProperty] private string street;
    [ObservableProperty] private string city;
    [ObservableProperty] private string postcode;

    public bool IsAdmin => _currentUserService.IsAdmin;

    public string FullName =>
        string.IsNullOrWhiteSpace(MiddleName) ? $"{FirstName} {Surname}" : $"{FirstName} {MiddleName} {Surname}";

    public int Id => User.Id;
    public string RoleName => User?.Role?.RoleName ?? "Unknown";
    
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    

    public UserViewModel(AppDbContext context, User user, ICurrentUserService currentUserService)
    {
        _context = context;
        User = user;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;

        LoadUserValues();
    }


    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin)); 
    }


    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            User = _context.Users.Include(u => u.Role).Single(u => u.Id == int.Parse(query["load"].ToString()));
            AvailableRoles = _context.Roles.ToList();
            LoadUserValues();
        }
    }
    

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
        User.MiddleName = MiddleName;
        User.Surname = Surname;
        User.Email = Email;
        User.PhoneNumber = PhoneNumber;
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


    [RelayCommand]
    public void Reload()
    {
    User = _context.Users.Include(u => u.Role).Single(u => u.Id == User.Id);
    AvailableRoles = _context.Roles.ToList();
    LoadUserValues();
}


    [RelayCommand]
    private async Task Delete()
    {
        if (!IsAdmin)
        {
            await Shell.Current.DisplayAlert("Access Denied", "You don’t have permission to perform this action.", "OK");
            return;
        }
            
        _context.Remove(User);
        _context.SaveChanges();
        await Shell.Current.GoToAsync($"..?deleted={User.Id}");
    }

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

}
