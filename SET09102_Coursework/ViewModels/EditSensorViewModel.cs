using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validation;


namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for editing an existing sensor. 
/// Provides bound properties, validation, and save/delete commands.
/// </summary>
[QueryProperty(nameof(Sensor), "Sensor")]
public partial class EditSensorViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;
    private readonly ISensorValidator _sensorValidator;

    /// <summary>List of all available sensor types for selection.</summary>
    public ObservableCollection<SensorType> SensorTypes { get; } = new();

    /// <summary>
    /// The sensor being edited. set via navigation.
    /// </summary>
    [ObservableProperty] 
    private Sensor sensor;

    [ObservableProperty] 
    private string name;

    [ObservableProperty] 
    private string firmwareVersion;

    /// <summary>
    /// Latitude input as string to allow binding with Entry.
    /// </summary>
    [ObservableProperty] 
    private string latitudeInput;
    
    /// <summary>
    /// Longitude input as string to allow binding with Entry.
    /// </summary>
    [ObservableProperty] 
    private string longitudeInput;

    /// <summary>
    /// Selected sensor type from the list of available types.
    /// </summary>
    /// <remarks>Bound to a Picker in the UI.</remarks>
    [ObservableProperty] 
    private SensorType selectedSensorType;

    /// <summary>
    /// Indicates if the sensor is active. Default is true.
    /// </summary>
    [ObservableProperty] 
    private bool isActive;

    [ObservableProperty] 
    private string errorMessage;

    [ObservableProperty] 
    private bool hasError;


    /// <summary>
    /// Constructs a new instance of the <see cref="EditSensorViewModel"/>  
    /// and loads the list of sensor types.
    /// </summary>
    public EditSensorViewModel(ISensorService sensorService, INavigationService navigationService, ISensorValidator sensorValidator)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
        _sensorValidator = sensorValidator;

        LoadSensorTypesAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Called when the <see cref="Sensor"/> property is set.  
    /// Pre-fills form fields with the current sensor values.
    /// </summary>
    partial void OnSensorChanged(Sensor value)
    {
        Name = value.Name;
        FirmwareVersion = value.FirmwareVersion;
        LatitudeInput = value.Latitude.ToString();
        LongitudeInput = value.Longitude.ToString();
        SelectedSensorType = SensorTypes.FirstOrDefault(t => t.Id == value.SensorTypeId);
        IsActive = value.IsActive;
    }

    /// <summary>
    /// Loads sensor types from the database and updates the <see cref="SensorTypes"/> list.
    /// </summary>
    private async Task LoadSensorTypesAsync()
    {
        var types = await _sensorService.GetSensorTypesAsync();
        SensorTypes.Clear();
        foreach (var type in types)
        {
            SensorTypes.Add(type);
        }

        if (Sensor is not null)
        {
            SelectedSensorType = SensorTypes.FirstOrDefault(t => t.Id == Sensor.SensorTypeId);
        }
    }

    /// <summary>
    /// Saves the sensor after validating inputs.  
    /// Updates the sensor in the database and shows success or error feedback.
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(FirmwareVersion) ||
            string.IsNullOrWhiteSpace(LatitudeInput) || string.IsNullOrWhiteSpace(LongitudeInput) ||
            SelectedSensorType == null)
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

        Sensor.Name = Name.Trim();
        Sensor.FirmwareVersion = FirmwareVersion.Trim();
        Sensor.Latitude = latitude;
        Sensor.Longitude = longitude;
        Sensor.SensorTypeId = SelectedSensorType.Id;
        Sensor.IsActive = IsActive;

        var validationResult = _sensorValidator.Validate(Sensor);
        if (!validationResult.IsValid)
        {
            ErrorMessage = validationResult.ErrorMessage;
            HasError = true;
            return;
        }

        try
        {
            var success = await _sensorService.UpdateSensorAsync(Sensor);

            if (success)
            {
                await Shell.Current.DisplayAlert("Success", "Sensor updated successfully!", "OK");
                await _navigationService.NavigateToAllSensorsAsync(refresh: true);
            }
            else
            {
                ErrorMessage = "Failed to update sensor.";
                HasError = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Unexpected error: {ex.Message}";
            HasError = true;
        }
    }


    /// <summary>
    /// Confirms and deletes the current sensor from the database.  
    /// Navigates back to the sensor list after deletion.
    /// </summary>
    [RelayCommand]
    private async Task Delete()
    {
        var confirm = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this sensor?", "Yes", "Cancel");
        if (!confirm) return;

        try
        {
            var success = await _sensorService.DeleteSensorAsync(Sensor.Id);
            if (success)
            {
                await Shell.Current.DisplayAlert("Deleted", "Sensor deleted successfully.", "OK");
                await _navigationService.NavigateToAllSensorsAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to delete sensor.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }
}