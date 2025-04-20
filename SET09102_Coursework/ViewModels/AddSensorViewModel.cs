using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validation;


namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for adding a new sensor. 
/// Handles input fields, validation, and saving the sensor via the sensor service.
/// </summary>
public partial class AddSensorViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly ISensorValidator _sensorValidator;
    private readonly INavigationService _navigationService;

    /// <summary>List of available sensor types shown in the dropdown.</summary>
    public ObservableCollection<SensorType> SensorTypes { get; } = new(); 

    [ObservableProperty] private string name;
    [ObservableProperty] private SensorType selectedSensorType;
    [ObservableProperty] private string firmwareVersion;
    [ObservableProperty] private decimal? longitude;
    [ObservableProperty] private decimal? latitude;
    [ObservableProperty] private bool isActive = true; 

    [ObservableProperty] private string errorMessage;
    [ObservableProperty] private bool hasError;

    
    /// <summary>
    /// Initialises the viewmodel and loads sensor types.
    /// </summary> 
    public AddSensorViewModel(
        ISensorService sensorService, 
        INavigationService navigationService, 
        ISensorValidator sensorValidator)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
        _sensorValidator = sensorValidator;

        LoadSensorTypesAsync().ConfigureAwait(false);
    }


    /// <summary>
    /// Loads available sensor types from the service and populates the SensorTypes collection.    
    /// </summary>
    private async Task LoadSensorTypesAsync()
    {
        var types = await _sensorService.GetSensorTypesAsync();
        SensorTypes.Clear();
        foreach (var type in types)
        {
            SensorTypes.Add(type);
        }
    }

    /// <summary>
    /// Checks whether any of the required input fields are missing or null.
    /// </summary>
    /// <returns>True if any required fields are missing; otherwise, false.</returns>
    private bool AreRequiredFieldsMissing()
    {
        return string.IsNullOrWhiteSpace(Name)
            || string.IsNullOrWhiteSpace(FirmwareVersion)
            || SelectedSensorType == null
            || Latitude == null
            || Longitude == null;
    }


    /// <summary>
    /// Validates and saves a new sensor to the database.
    /// Displays an error if the operation fails.
    /// </summary> 
    [RelayCommand]
    private async Task SaveSensor()
    {
        HasError = false;
    ErrorMessage = string.Empty;

    // ✅ Validate fields BEFORE creating the Sensor object
    if (AreRequiredFieldsMissing())
    {
        ErrorMessage = "Please fill in all required fields.";
        HasError = true;
        return;
    }

        var newSensor = new Sensor
        {
            Name = Name.Trim(),
            FirmwareVersion = FirmwareVersion.Trim(),
            SensorTypeId = SelectedSensorType.Id,
            Longitude = Longitude.Value,
            Latitude = Latitude.Value,
            IsActive = IsActive
        };

        var validationResult = _sensorValidator.Validate(newSensor);
        if (!validationResult.IsValid)
        {
            ErrorMessage = validationResult.ErrorMessage;
            HasError = true;
            return;
        }

        var success = await _sensorService.AddSensorAsync(newSensor);

    if (success)
    {
        await Shell.Current.DisplayAlert("Success", "Sensor added successfully!", "OK");
        await _navigationService.NavigateToAllSensorsAsync();
    }
    else
    {
        ErrorMessage = "An error occurred while saving the sensor.";
        HasError = true;
    }
    }


    /// <summary>
    /// Cancel and return to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}