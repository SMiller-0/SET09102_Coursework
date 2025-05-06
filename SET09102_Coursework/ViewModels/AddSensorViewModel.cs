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
/// Handles input fields, performs validation, and saves the sensor using the sensor service.
/// </summary>
public partial class AddSensorViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly ISensorValidator _sensorValidator;
    private readonly INavigationService _navigationService;

    /// <summary>List of all available sensor types for selection.</summary>
    public ObservableCollection<SensorType> SensorTypes { get; } = new(); 

    [ObservableProperty] 
    private string name;
    
    [ObservableProperty] 
    private SensorType selectedSensorType;

    [ObservableProperty] 
    private string firmwareVersion;

    /// <summary>
    /// Longitude input as a string, allowing binding to an Entry field.
    // </summary>
    [ObservableProperty] 
    private string longitudeInput;

    /// <summary>
    /// Latitude input as a string, allowing binding to an Entry field.
    /// </summary>
    [ObservableProperty] 
    private string latitudeInput;
    
    [ObservableProperty] 
    private bool isActive = true; 

    [ObservableProperty] 
    private string errorMessage;

    [ObservableProperty] 
    private bool hasError;

    
    /// <summary>
    /// Initialises the viewmodel and loads sensor types.
    /// </summary> 
    /// <param name="sensorService">Service for managing sensors.</param>
    /// <param name="navigationService">Service for navigating between pages.</param>
    /// <param name="sensorValidator">Validator for sensor data.</param>
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
    /// <returns><c>true</c> if any required fields are empty or null; otherwise, <c>false</c>.</returns>    
    private bool AreRequiredFieldsMissing()
    {
        return string.IsNullOrWhiteSpace(Name)
            || string.IsNullOrWhiteSpace(FirmwareVersion)
            || SelectedSensorType == null
            || string.IsNullOrWhiteSpace(LatitudeInput)
            || string.IsNullOrWhiteSpace(LongitudeInput);
    }

    /// <summary>
    /// Validates input, creates a new sensor object, and attempts to save it.
    /// Displays error messages or success notifications as appropriate.
    /// </summary>
    [RelayCommand]
    private async Task SaveSensor()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        if (AreRequiredFieldsMissing())
        {
            ErrorMessage = "Please fill in all required fields.";
            HasError = true;
            return;
        }

        if (!decimal.TryParse(LatitudeInput, out var latitude))
        {
            ErrorMessage = "Latitude must be a valid number.";
            HasError = true;
            return;
        }

        if (!decimal.TryParse(LongitudeInput, out var longitude))
        {
            ErrorMessage = "Longitude must be a valid number.";
            HasError = true;
            return;
        }

        var newSensor = new Sensor
        {
            Name = Name.Trim(),
            FirmwareVersion = FirmwareVersion.Trim(),
            SensorTypeId = SelectedSensorType.Id,
            Longitude = longitude,
            Latitude = latitude,
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
    /// Cancels the operation and navigates back to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}