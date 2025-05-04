using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public interface INavigationService
{
    Task NavigateToSensorDetailsAsync(Sensor sensor);
    Task NavigateToSensorSettingsAsync(Sensor sensor);
    Task NavigateToAllSensorsAsync(bool refresh = false);
    Task NavigateToUpdateFirmwareAsync(Sensor sensor);
    Task NavigateToUpdateSettingsAsync(Sensor sensor);
    Task NavigateToAddNewSensorAsync();
    Task NavigateToEditSensorAsync(Sensor sensor);
    Task NavigateToSensorStatusAsync();
    Task NavigateToCreateTicketAsync(Sensor sensor);
    Task NavigateToTicketDetailsAsync(SensorTicket ticket);
    Task NavigateToSensorReportAsync();
    Task NavigateToTrendReportAsync(Sensor sensor);

}

