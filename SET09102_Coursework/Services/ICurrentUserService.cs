using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public interface ICurrentUserService
{
    User? LoggedInUser { get; }
    void SetUser(User user);
    void Logout();
    bool IsAdmin { get; }
    bool IsOperationsManager { get; }
    event EventHandler? UserChanged;
}
