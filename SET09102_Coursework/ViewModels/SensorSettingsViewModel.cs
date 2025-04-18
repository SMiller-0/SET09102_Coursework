using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

[QueryProperty(nameof(Sensor), "sensor")]
public partial class SensorSettingsViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;
    
    [ObservableProperty]
    private Sensor sensor;

    [ObservableProperty]
    private ObservableCollection<Settings> sensorSettings = new();

    public SensorSettingsViewModel(ISensorService sensorService)
    {
        _sensorService = sensorService;
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
}
