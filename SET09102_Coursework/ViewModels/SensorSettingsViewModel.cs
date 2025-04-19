using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

[QueryProperty(nameof(Sensor), "sensor")]
public partial class SensorSettingsViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;
    private readonly ICurrentUserService _currentUserService;

    [ObservableProperty]
    private Sensor sensor;

    [ObservableProperty]
    private ObservableCollection<Settings> sensorSettings = new();

    [ObservableProperty]
    private bool isAdmin;

    public SensorSettingsViewModel(
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
    private async Task UpdateSettings()
    {
        await _navigationService.NavigateToUpdateSettingsAsync(Sensor);
    }
}
