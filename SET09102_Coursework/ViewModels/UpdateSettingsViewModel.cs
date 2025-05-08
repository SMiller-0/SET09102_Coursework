using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validation;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

[QueryProperty(nameof(Sensor), "sensor")]
public partial class UpdateSettingsViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;
    private readonly ISettingsValidator _settingsValidator;

    [ObservableProperty]
    private Sensor sensor;

    [ObservableProperty]
    private ObservableCollection<Settings> sensorSettings = new();

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool hasError;

    public UpdateSettingsViewModel(
        ISensorService sensorService,
        INavigationService navigationService,
        ISettingsValidator settingsValidator)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
        _settingsValidator = settingsValidator;
    }

    partial void OnSensorChanged(Sensor value)
    {
        if (value != null)
        {
            LoadSettingsAsync().ConfigureAwait(false);
        }
    }

    private async Task LoadSettingsAsync()
    {
        var settings = await _sensorService.GetSensorSettingsAsync(Sensor.Id);
        SensorSettings.Clear();
        foreach (var setting in settings)
        {
            SensorSettings.Add(setting);
        }
    }

    [RelayCommand]
    private async Task SaveChanges()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            var validationResult = _settingsValidator.ValidateCollection(SensorSettings);
            if (!validationResult.IsValid)
            {
                ErrorMessage = validationResult.ErrorMessage;
                HasError = true;
                return;
            }

            var success = await _sensorService.UpdateSensorSettingsAsync(SensorSettings);
            
            if (success)
            {
                await Shell.Current.DisplayAlert(
                    "Success",
                    "Settings have been updated successfully.",
                    "OK");

                await _navigationService.NavigateToAllSensorsAsync();
            }
            else
            {
                ErrorMessage = "Failed to update settings. Please try again.";
                HasError = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
            HasError = true;
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}

