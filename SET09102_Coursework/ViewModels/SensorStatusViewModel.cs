using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public partial class SensorStatusViewModel : ObservableObject, IDisposable
{
    private readonly ISensorRefreshService _refreshService;
    private const int REFRESH_INTERVAL_SECONDS = 5;

    public ObservableCollection<Sensor> Sensors { get; } = new();

    [ObservableProperty]
    private bool isRefreshing;

    public SensorStatusViewModel(ISensorRefreshService refreshService)
    {
        _refreshService = refreshService;
        _refreshService.SensorsRefreshed += OnSensorsRefreshed;
        
        LoadSensorsCommand.Execute(null);
        _refreshService.StartAutoRefresh(REFRESH_INTERVAL_SECONDS);
    }

    private void OnSensorsRefreshed(object? sender, IEnumerable<Sensor> sensors)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Sensors.Clear();
            foreach (var sensor in sensors)
            {
                Sensors.Add(sensor);
            }
        });
    }

    [RelayCommand]
    private async Task LoadSensors()
    {
        try
        {
            IsRefreshing = true;
            await _refreshService.RefreshSensors();
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

    public void Dispose()
    {
        _refreshService.Dispose();
        GC.SuppressFinalize(this);
    }
}



