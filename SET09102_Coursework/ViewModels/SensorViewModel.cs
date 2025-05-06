using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying sensor details and providing actions  
/// like editing, updating firmware/settings, or creating a ticket.
/// </summary>
[QueryProperty(nameof(Sensor), "sensor")]
public partial class SensorViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// The selected sensor to display and act upon.
    /// Populated via Shell navigation query.
    /// </summary>
    [ObservableProperty]
    private Sensor sensor;

    /// <summary>
    /// Whether the current user has administrator privileges.
    /// Determines access to admin-only commands.
    /// </summary>
    [ObservableProperty]
    private bool isAdmin;

    /// <summary>
    /// Initialises a new instance of the <see cref="SensorViewModel"/>.
    /// Subscribes to user role changes to keep the admin status updated.
    /// </summary>
    public SensorViewModel(
        INavigationService navigationService,
        ICurrentUserService currentUserService)
    {
        _navigationService = navigationService;
        _currentUserService = currentUserService;
        
        IsAdmin = _currentUserService.IsAdmin;
        _currentUserService.UserChanged += OnUserChanged;
    }

    /// <summary>
    /// Updates the <see cref="IsAdmin"/> flag if the current user changes.
    /// </summary>
    private void OnUserChanged(object? sender, EventArgs e)
    {
        IsAdmin = _currentUserService.IsAdmin;
    }

    /// <summary>
    /// Navigates to the sensor settings page for this sensor.
    /// </summary>
    [RelayCommand]
    private async Task UpdateSettings()
    {
        await _navigationService.NavigateToSensorSettingsAsync(Sensor);
    }

    /// <summary>
    /// Navigates to the firmware update page for this sensor.
    /// </summary>
    [RelayCommand]
    private async Task UpdateFirmware()
    {
        await _navigationService.NavigateToUpdateFirmwareAsync(Sensor);
    }

    /// <summary>
    /// Navigates to the edit sensor page.
    /// </summary>
    [RelayCommand]
    private async Task EditSensor()
    {
        await _navigationService.NavigateToEditSensorAsync(Sensor);
    }  

    /// <summary>
    /// Navigates to the create ticket page for this sensor.
    /// </summary>
    [RelayCommand]
    private async Task CreateTicket()
    {
        await _navigationService.NavigateToCreateTicketAsync(Sensor);
    }
 
}


