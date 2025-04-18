using SET09102_Coursework.Models;
using SET09102_Coursework.Views;

namespace SET09102_Coursework.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToSensorDetailsAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(SensorPage), parameters);
    }

    public async Task NavigateToSensorSettingsAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(SensorSettingsPage), parameters);
    }

    public async Task NavigateToAllSensorsAsync()
    {
        await Shell.Current.GoToAsync($"///SensorDashboardPage/{nameof(AllSensorsPage)}");
    }

    public async Task NavigateToUpdateFirmwareAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(UpdateFirmwarePage), parameters);
    }
}


