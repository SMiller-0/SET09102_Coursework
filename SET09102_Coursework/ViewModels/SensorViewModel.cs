using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
namespace SET09102_Coursework.ViewModels;
using SET09102_Coursework.Data;

[QueryProperty(nameof(SelectedSensor), "SelectedSensor")]
public partial class SensorViewModel : ObservableObject
{
    [ObservableProperty]
    private Sensor selectedSensor;
}


