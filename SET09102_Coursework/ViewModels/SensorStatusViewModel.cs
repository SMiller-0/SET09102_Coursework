using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public partial class SensorStatusViewModel : ObservableObject, IDisposable
{
    private readonly ISensorService _sensorService;
    private readonly ITimerService _timerService;
    private const int REFRESH_INTERVAL_SECONDS = 5;

    public ObservableCollection<Sensor> Sensors { get; } = new();

    public SensorStatusViewModel(
        ISensorService sensorService,
        ITimerService timerService)
    {
        _sensorService = sensorService;
        _timerService = timerService;
        
        // Initial load
        LoadSensorsCommand.Execute(null);
        
        // Start auto-refresh immediately
        _timerService.Start(TimeSpan.FromSeconds(REFRESH_INTERVAL_SECONDS), 
            async () => await LoadSensorsCommand.ExecuteAsync(null));
    }

    [RelayCommand]
    private async Task LoadSensors()
    {
        try
        {
            var sensors = await _sensorService.GetSensorsByTypeAsync(null);
            
            Sensors.Clear();
            foreach (var sensor in sensors)
            {
                Sensors.Add(sensor);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to load sensors.", "OK");
        }
    }

    public void Dispose()
    {
        _timerService.Dispose();
        GC.SuppressFinalize(this);
    }
}



