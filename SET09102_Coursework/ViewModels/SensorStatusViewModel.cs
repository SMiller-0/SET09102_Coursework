using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Services;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.ViewModels;

public partial class SensorStatusViewModel : ObservableObject
{
    private readonly ISensorService _sensorService;

    public ObservableCollection<Sensor> Sensors { get; } = new();

    public SensorStatusViewModel(ISensorService sensorService)
    {
        _sensorService = sensorService;
        LoadSensorsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadSensors()
    {
        try
        {
            Sensors.Clear();
            var sensors = await _sensorService.GetSensorsByTypeAsync(null);
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
}



