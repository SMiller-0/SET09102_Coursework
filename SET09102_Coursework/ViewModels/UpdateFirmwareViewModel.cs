using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validators;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for updating the firmware version of a sensor.
/// Receives a sensor object through query parameter navigation.
/// </summary>
[QueryProperty(nameof(Sensor), "sensor")]
public partial class UpdateFirmwareViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;

    /// <summary>
    /// The sensor whose firmware is being updated.
    /// Passed from navigation as a query parameter.
    /// </summary>
    [ObservableProperty]
    private Sensor sensor;

    /// <summary>
    /// The new firmware version to be applied to the sensor.
    /// Bound to the input field in the view.
    /// </summary>
    [ObservableProperty]
    private string newVersion = string.Empty;

    /// <summary>
    /// Error message to display when validation fails.
    /// </summary>
    [ObservableProperty]
    private string errorMessage = string.Empty;

    /// <summary>
    /// Flag indicating whether an error has occurred.
    /// Controls visibility of error message in the view.
    /// </summary>
    [ObservableProperty]
    private bool hasError;

    /// <summary>
    /// Initializes a new instance of the UpdateFirmwareViewModel class.
    /// </summary>
    /// <param name="sensorService">Service for sensor operations</param>
    /// <param name="navigationService">Service for navigation between pages</param>
    public UpdateFirmwareViewModel(
        ISensorService sensorService,
        INavigationService navigationService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
    }

    /// <summary>
    /// Updates the firmware version of the sensor.
    /// Validates the version format before attempting the update.
    /// Displays success or error messages based on the result.
    /// </summary>
    [RelayCommand]
    private async Task UpdateFirmware()
    {
        // Reset error state
        HasError = false;
        ErrorMessage = string.Empty;

        // Validate firmware version format
        if (!FirmwareVersionValidator.IsValid(NewVersion))
        {
            ErrorMessage = "Invalid version format. Please use X.Y.Z format (e.g., 1.0.0)";
            HasError = true;
            return;
        }

        // Attempt to update the firmware version
        var success = await _sensorService.UpdateFirmwareVersionAsync(Sensor.Id, NewVersion);

        if (success)
        {
            // Show success message and navigate back to sensors list
            await Shell.Current.DisplayAlert(
                "Success",
                "Firmware version has been updated successfully.",
                "OK");

            await _navigationService.NavigateToAllSensorsAsync();
        }
        else
        {
            // Show error message
            ErrorMessage = "Failed to update firmware version. Please try again.";
            HasError = true;
        }
    }

    /// <summary>
    /// Cancels the firmware update operation and navigates back to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
