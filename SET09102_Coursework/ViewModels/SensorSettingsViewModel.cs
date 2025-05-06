using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for the Sensor Settings page that displays and manages sensor settings.
/// Receives a sensor object through query parameter navigation.
/// </summary>
[QueryProperty(nameof(Sensor), "sensor")]
public partial class SensorSettingsViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// The sensor whose settings are being displayed.
    /// Passed from navigation as a query parameter.
    /// </summary>
    [ObservableProperty]
    private Sensor sensor;

    /// <summary>
    /// Collection of settings for the current sensor.
    /// Displayed in the UI and updated when the sensor changes.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Settings> sensorSettings = new();

    /// <summary>
    /// Flag indicating whether the current user has admin privileges.
    /// Controls visibility of admin-only features in the UI.
    /// </summary>
    [ObservableProperty]
    private bool isAdmin;

    /// <summary>
    /// Initializes a new instance of the SensorSettingsViewModel class.
    /// Sets up event handlers and initializes user permissions.
    /// </summary>
    /// <param name="sensorService">Service for sensor operations</param>
    /// <param name="navigationService">Service for navigation between pages</param>
    /// <param name="currentUserService">Service for current user information</param>
    public SensorSettingsViewModel(
        ISensorService sensorService,
        INavigationService navigationService,
        ICurrentUserService currentUserService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
        _currentUserService = currentUserService;
        
        // Initialize admin status and subscribe to user changes
        IsAdmin = _currentUserService.IsAdmin;
        _currentUserService.UserChanged += OnUserChanged;
    }

    /// <summary>
    /// Event handler for when the current user changes.
    /// Updates the IsAdmin property to reflect the new user's permissions.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    private void OnUserChanged(object? sender, EventArgs e)
    {
        IsAdmin = _currentUserService.IsAdmin;
    }

    /// <summary>
    /// Handler for when the Sensor property changes.
    /// Loads settings for the new sensor.
    /// </summary>
    /// <param name="value">The new sensor value</param>
    partial void OnSensorChanged(Sensor value)
    {
        if (value != null)
        {
            LoadSettingsAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Loads settings for the current sensor from the sensor service.
    /// Clears and repopulates the SensorSettings collection.
    /// </summary>
    /// <returns>A task representing the asynchronous operation</returns>
    private async Task LoadSettingsAsync()
    {
        var settings = await _sensorService.GetSensorSettingsAsync(Sensor.Id);
        SensorSettings.Clear();
        foreach (var setting in settings)
        {
            SensorSettings.Add(setting);
        }
    }

    /// <summary>
    /// Command to navigate to the Update Settings page.
    /// Only available to users with admin privileges.
    /// </summary>
    /// <returns>A task representing the asynchronous operation</returns>
    [RelayCommand]
    private async Task UpdateSettings()
    {
        await _navigationService.NavigateToUpdateSettingsAsync(Sensor);
    }
}
