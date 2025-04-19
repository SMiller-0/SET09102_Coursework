using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validators;

namespace SET09102_Coursework.ViewModels;

[QueryProperty(nameof(Sensor), "sensor")]
public partial class UpdateFirmwareViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private Sensor sensor;

    [ObservableProperty]
    private string newVersion = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool hasError;

    public UpdateFirmwareViewModel(
        ISensorService sensorService,
        INavigationService navigationService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task UpdateFirmware()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        if (!FirmwareVersionValidator.IsValid(NewVersion))
        {
            ErrorMessage = "Invalid version format. Please use X.Y.Z format (e.g., 1.0.0)";
            HasError = true;
            return;
        }

        var success = await _sensorService.UpdateFirmwareVersionAsync(Sensor.Id, NewVersion);

        if (success)
        {
            await Shell.Current.DisplayAlert(
                "Success",
                "Firmware version has been updated successfully.",
                "OK");

            await _navigationService.NavigateToAllSensorsAsync();
        }
        else
        {
            ErrorMessage = "Failed to update firmware version. Please try again.";
            HasError = true;
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
