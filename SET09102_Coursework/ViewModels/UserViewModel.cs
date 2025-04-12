using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
namespace SET09102_Coursework.ViewModels;
using SET09102_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;


public partial class UserViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public User user; 

    private AppDbContext _context;
   
    public int Id => user.Id;
    public string FirstName => user.FirstName;
    public string Surname => user.Surname;
    public string Email => user.Email;
    //public string RoleName => User?.Role?.Name ?? "Unknown";

    public UserViewModel(AppDbContext appDbContext)
    {
        _context = appDbContext;
        user = new User();
    }

        public UserViewModel(AppDbContext context, User user)
    {
        _context = context;
        this.user = user;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            User = _context.Users.Include(u => u.Role).Single(n => n.Id == int.Parse(query["load"].ToString()));

        }
    }

    [RelayCommand]
    private async Task Save()
    {
        _context.SaveChanges();
        await Shell.Current.GoToAsync($"..?saved={User.Id}");
    }

    public void Reload()
    {
        _context.Entry(User).Reload();
    }

    [RelayCommand]
    private async Task Delete()
    {
        _context.Remove(User);
        _context.SaveChanges();
        await Shell.Current.GoToAsync($"..?deleted={User.Id}");
    }
}
