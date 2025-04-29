using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;
using SET09102_Coursework.Data;

namespace SET09102_Coursework.ViewModels;

public partial class AllSensorsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Sensor> sensors;

    public AllSensorsViewModel()
    {
        Sensors = new ObservableCollection<Sensor>
        {};
    }

    [RelayCommand]
    private async Task ViewSensorDetails(Sensor sensor)
    {
        await Shell.Current.GoToAsync(nameof(SensorPage), true, new Dictionary<string, object>
        {
            { "SelectedSensor", sensor }
        });
    }
    
}