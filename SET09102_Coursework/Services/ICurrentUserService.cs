using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// Provides information about the currently logged-in user,
/// and notifies subscribers when the user state changes.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// The user currently logged into the application.
    /// Returns <c>null</c> if no user is logged in.
    /// </summary>
    User? LoggedInUser { get; }

    /// <summary>
    /// Sets the currently logged-in user.
    /// </summary>
    void SetUser(User user);

    /// <summary>
    /// Clears the current user and logs out.
    /// </summary>
    void Logout();
    
    /// <summary>
    /// Indicates whether the current user has an Administrator role.
    /// </summary>
    bool IsAdmin { get; }

    /// <summary>
    /// Indicates whether the current user has an Operations Manager role.
    /// </summary>
    bool IsOperationsManager { get; }

    /// <summary>
    /// Indicates whether the current user has an Environmental Scientist role.
    /// </summary>
    bool IsEnvScientist {get; }

    /// <summary>
    /// Raised whenever the current user is changed or logged out.
    /// </summary>
    event EventHandler? UserChanged;
}
