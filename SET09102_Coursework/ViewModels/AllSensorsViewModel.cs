using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

public partial class AllSensorsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Sensor> sensors;

    public AllSensorsViewModel()
    {
        Sensors = new ObservableCollection<Sensor>
        {
            new Sensor { Id = 111, Name = "Sensor A", Latitude = "-3.1883° W", Longitude = "55.9533° N", Type = SensorType.Air, IsActive = true },
            new Sensor { Id = 112, Name = "Sensor B", Latitude = "-3.1843° W", Longitude = "57.5733° N", Type = SensorType.Water, IsActive = true },
            new Sensor { Id = 113, Name = "Sensor C", Latitude = "-3.1683° W", Longitude = "55.9533° N", Type = SensorType.Water, IsActive = false },
            new Sensor { Id = 114, Name = "Sensor D", Latitude = "-3.1383° W", Longitude = "59.9883° N", Type = SensorType.Weather, IsActive = true },
        };
    }


    [RelayCommand]
    private async Task ViewSensorDetails(Sensor sensor)
    {
        await Shell.Current.GoToAsync(nameof(SensorPage), true, new Dictionary<string, object>
        {
            { "Sensor", sensor }
        });
    }

}