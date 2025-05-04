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
    private readonly ICurrentUserService _currentUserService;

    [ObservableProperty]
    private Sensor sensor;

    [ObservableProperty]
    private bool isAdmin;

    public SensorViewModel(
        ISensorService sensorService, 
        INavigationService navigationService,
        ICurrentUserService currentUserService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
        _currentUserService = currentUserService;
        
        IsAdmin = _currentUserService.IsAdmin;
        _currentUserService.UserChanged += OnUserChanged;
    }

    private void OnUserChanged(object? sender, EventArgs e)
    {
        IsAdmin = _currentUserService.IsAdmin;
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

    [RelayCommand]
    private async Task EditSensor()
    {
        await _navigationService.NavigateToEditSensorAsync(Sensor);
    }  

    [RelayCommand]
    private async Task CreateTicket()
    {
        await _navigationService.NavigateToCreateTicketAsync(Sensor);
    }
 
}


