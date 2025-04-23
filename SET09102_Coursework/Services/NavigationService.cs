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

    public async Task NavigateToAllSensorsAsync(bool refresh = false)
    {
        var uri = refresh
        ? "///SensorDashboardPage/AllSensorsPage?refresh=true"
        : $"///SensorDashboardPage/{nameof(AllSensorsPage)}";
        
        await Shell.Current.GoToAsync(uri);
    }

    public async Task NavigateToUpdateFirmwareAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(UpdateFirmwarePage), parameters);
    }

    public async Task NavigateToUpdateSettingsAsync(Sensor sensor)
    {
        if (sensor == null) return;
        
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(UpdateSettingsPage), parameters);
    }

    public async Task NavigateToAddNewSensorAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddSensorPage));
    }

    public async Task NavigateToEditSensorAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(EditSensorPage), parameters);
    }

    public async Task NavigateToSensorStatusAsync()
    {
        await Shell.Current.GoToAsync(nameof(SensorStatusPage));
    }

}


