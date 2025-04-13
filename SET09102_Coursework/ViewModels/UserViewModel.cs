using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public partial class UserViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public User user; 
    [ObservableProperty]
    private string roleName;
    public bool IsAdmin => _currentUserService.IsAdmin;


    private AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

   
    public int Id => user.Id;
    public string FirstName => user.FirstName;
    public string Surname => user.Surname;
    public string Email => user.Email;

    public string FullName =>
    string.IsNullOrWhiteSpace(user?.MiddleName)
        ? $"{user?.FirstName} {user?.Surname}"
        : $"{user?.FirstName} {user.MiddleName} {user.Surname}";


    public UserViewModel(AppDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _context = appDbContext;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;
        user = new User();

        
    }

    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin)); // 🔁 update UI
    }

        public UserViewModel(AppDbContext context, User user, ICurrentUserService currentUserService)
    {
        _context = context;
        this.user = user;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            User = _context.Users.Include(u => u.Role).Single(n => n.Id == int.Parse(query["load"].ToString()));
            RoleName = User?.Role?.RoleName ?? "Unknown";
            OnPropertyChanged(nameof(FullName));
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (!_currentUserService.IsAdmin)
            {
                await Shell.Current.DisplayAlert("Access Denied", "You don’t have permission to perform this action.", "OK");
                return;
            }

        _context.SaveChanges();
        await Shell.Current.DisplayAlert("Saved", "User details updated.", "OK");
        await Shell.Current.GoToAsync($"..?saved={User.Id}");
    }

    public void Reload()
    {
        _context.Entry(User).Reload();
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (!_currentUserService.IsAdmin)
        {
            await Shell.Current.DisplayAlert("Access Denied", "You don’t have permission to perform this action.", "OK");
            return;
        }
            
        _context.Remove(User);
        _context.SaveChanges();
        await Shell.Current.GoToAsync($"..?deleted={User.Id}");
    }
}
