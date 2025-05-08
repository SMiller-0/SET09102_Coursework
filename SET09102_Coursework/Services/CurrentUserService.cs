using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <inheritdoc/>
public class CurrentUserService : ICurrentUserService
{
    /// <inheritdoc/>
    public User? LoggedInUser { get; private set; }

    /// <inheritdoc/>
    public bool IsAdmin => LoggedInUser?.Role?.RoleName == "Administrator";

    /// <inheritdoc/>
    public bool IsOperationsManager => LoggedInUser?.Role?.RoleName == "Operations Manager";

    /// <inheritdoc/>
    public bool IsEnvScientist => LoggedInUser?.Role?.RoleName == "Environmental Scientist";

    /// <inheritdoc/>
    public event EventHandler? UserChanged;

    /// <inheritdoc/>
    public void SetUser(User user)
    {
        LoggedInUser = user;
        UserChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public void Logout()
    {
        LoggedInUser = null;
        UserChanged?.Invoke(this, EventArgs.Empty);
    }
}
