using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

[QueryProperty(nameof(Sensor), "sensor")]
public partial class SensorViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private Sensor sensor;

    public SensorViewModel(ISensorService sensorService, INavigationService navigationService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task UpdateSettings()
    {
        await _navigationService.NavigateToSensorSettingsAsync(Sensor);
    }

    [RelayCommand]
    private async Task UpdateFirmware()
    {
        await _navigationService.NavigateToUpdateFirmwareAsync(Sensor);
    }
}


