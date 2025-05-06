using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for the sensor dashboard.  
/// Provides navigation commands for sensor-related pages.
/// </summary>
public partial class SensorDashboardViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    /// <summary>
    /// Constructs a new <see cref="SensorDashboardViewModel"/>.
    /// </summary>
    /// <param name="navigationService">Service for handling page navigation.</param>
    public SensorDashboardViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <summary>
    /// Navigates to the page that lists all sensors.
    /// </summary>
    [RelayCommand]
    private async Task ViewSensors()
    {
        await _navigationService.NavigateToAllSensorsAsync();
    }

    /// <summary>
    /// Navigates to the page for adding a new sensor.
    /// </summary>
    [RelayCommand]
    private async Task AddNewSensor()
    {
        await _navigationService.NavigateToAddNewSensorAsync();
    }

    /// <summary>
    /// Navigates to the sensor status overview page.
    /// </summary>
    [RelayCommand]
    private async Task ViewSensorStatus()
    {
        await _navigationService.NavigateToSensorStatusAsync();
    }

    /// <summary>
    /// Navigates to the sensor report page.
    /// </summary>
    [RelayCommand]
    private async Task SensorReport()
    {
        await _navigationService.NavigateToSensorReportAsync();
    }
}



