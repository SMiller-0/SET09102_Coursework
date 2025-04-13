using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public class CurrentUserService : ICurrentUserService
{
    public User? LoggedInUser { get; private set; }
    public bool IsAdmin => LoggedInUser?.Role?.RoleName == "Administrator";
    public event EventHandler? UserChanged;

    public void SetUser(User user)
    {
        LoggedInUser = user;
        UserChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Logout()
    {
        LoggedInUser = null;
        UserChanged?.Invoke(this, EventArgs.Empty);
    }
}
