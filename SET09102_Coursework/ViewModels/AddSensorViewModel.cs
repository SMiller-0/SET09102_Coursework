using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;


namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for adding a new sensor. 
/// Handles input fields, validation, and saving the sensor via the sensor service.
/// </summary>
public partial class AddSensorViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;

    /// <summary>List of available sensor types shown in the dropdown.</summary>
    public ObservableCollection<SensorType> SensorTypes { get; } = new(); 

    [ObservableProperty] private string name;
    [ObservableProperty] private SensorType selectedSensorType;
    [ObservableProperty] private string firmwareVersion;
    [ObservableProperty] private decimal longitude;
    [ObservableProperty] private decimal latitude;
    [ObservableProperty] private bool isActive = true; 

    [ObservableProperty] private string errorMessage;
    [ObservableProperty] private bool hasError;

    
    /// <summary>
    /// Initialises the viewmodel and loads sensor types.
    /// </summary> 
    public AddSensorViewModel(ISensorService sensorService, INavigationService navigationService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;

        LoadSensorTypesAsync().ConfigureAwait(false);
    }


    /// <summary>
    /// Loads available sensor types from the service.    
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
    /// Validates and saves a new sensor to the database.
    /// Displays an error if the operation fails.
    /// </summary> 
    [RelayCommand]
    private async Task SaveSensor()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        // Validation
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(FirmwareVersion) || SelectedSensorType == null)
        {
            ErrorMessage = "Please fill in all fields.";
            HasError = true;
            return;
        }

        var newSensor = new Sensor
        {
            Name = Name.Trim(),
            FirmwareVersion = FirmwareVersion.Trim(),
            SensorTypeId = SelectedSensorType.Id,
            Longitude = Longitude,
            Latitude = Latitude,
            IsActive = IsActive
        };

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