using SET09102_Coursework.Models;
using SET09102_Coursework.Views;

namespace SET09102_Coursework.Services;

public class NavigationService : INavigationService
{
    private Dictionary<string, object> CreateSensorParameters(Sensor sensor) =>
        new() { { "sensor", sensor } };

    public async Task NavigateToSensorDetailsAsync(Sensor sensor) =>
        await Shell.Current.GoToAsync(nameof(SensorPage), CreateSensorParameters(sensor));

    public async Task NavigateToSensorSettingsAsync(Sensor sensor) =>
        await Shell.Current.GoToAsync(nameof(SensorSettingsPage), CreateSensorParameters(sensor));

    public async Task NavigateToAllSensorsAsync(bool refresh = false)
    {
        var uri = refresh ? 
            "///SensorDashboardPage/AllSensorsPage?refresh=true" : 
            "///SensorDashboardPage/AllSensorsPage";
        await Shell.Current.GoToAsync(uri);
    }

    public async Task NavigateToUpdateFirmwareAsync(Sensor sensor) =>
        await Shell.Current.GoToAsync(nameof(UpdateFirmwarePage), CreateSensorParameters(sensor));

    public async Task NavigateToUpdateSettingsAsync(Sensor sensor)
    {
        if (sensor == null) return;
        await Shell.Current.GoToAsync(nameof(UpdateSettingsPage), CreateSensorParameters(sensor));
    }

    public async Task NavigateToAddNewSensorAsync() =>
        await Shell.Current.GoToAsync(nameof(AddSensorPage));

    public async Task NavigateToEditSensorAsync(Sensor sensor) =>
        await Shell.Current.GoToAsync(nameof(EditSensorPage), CreateSensorParameters(sensor));

    public async Task NavigateToSensorStatusAsync() =>
        await Shell.Current.GoToAsync(nameof(SensorStatusPage));
}


