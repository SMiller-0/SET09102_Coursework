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

    [ObservableProperty]
    private bool isRefreshing;

    public SensorStatusViewModel(
        ISensorService sensorService,
        ITimerService timerService)
    {
        _sensorService = sensorService;
        _timerService = timerService;
        
 
        LoadSensorsCommand.Execute(null);
        
        _timerService.Start(TimeSpan.FromSeconds(REFRESH_INTERVAL_SECONDS), 
            async () => await RefreshSensors());
    }

    [RelayCommand]
    private async Task LoadSensors()
    {
        try
        {
            IsRefreshing = true;
            await RefreshSensors();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to load sensors.", "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async Task RefreshSensors()
    {
        try
        {
            var sensors = await _sensorService.GetSensorsByTypeAsync(null);
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Sensors.Clear();
                foreach (var sensor in sensors)
                {
                    Sensors.Add(sensor);
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error refreshing sensors: {ex.Message}");
        }
    }

    public void Dispose()
    {
        _timerService.Dispose();
        GC.SuppressFinalize(this);
    }
}



