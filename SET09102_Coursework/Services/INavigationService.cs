using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public interface INavigationService
{
    Task NavigateToSensorDetailsAsync(Sensor sensor);
    Task NavigateToSensorSettingsAsync(Sensor sensor);
    Task NavigateToAllSensorsAsync();
    Task NavigateToUpdateFirmwareAsync(Sensor sensor);
}

