using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// Defines navigation methods for moving between views in the application.
/// </summary>
public interface INavigationService
{
    /// <summary>Navigates to the sensor detail page for a specific sensor.</summary>
    /// <param name="sensor">The sensor to view details for.</param>
    Task NavigateToSensorDetailsAsync(Sensor sensor);

    /// <summary>Navigates to the sensor settings page.</summary>
    /// <param name="sensor">The sensor to view settings for.</param>
    Task NavigateToSensorSettingsAsync(Sensor sensor);

    /// <summary>Navigates to the full list of sensors. Optionally refreshes the list.</summary>
    /// <param name="refresh">Whether to refresh the list of sensors.</param>
    Task NavigateToAllSensorsAsync(bool refresh = false);

    /// <summary>Navigates to the firmware update page for a sensor.</summary>
    /// <param name="sensor">The sensor to update firmware for.</param>
    Task NavigateToUpdateFirmwareAsync(Sensor sensor);

    /// <summary>Navigates to the settings update page for a sensor.</summary>
    /// <param name="sensor">The sensor to update settings for.</param>
    Task NavigateToUpdateSettingsAsync(Sensor sensor);

    /// <summary>Navigates to the "Add New Sensor" page.</summary>
    Task NavigateToAddNewSensorAsync();

    /// <summary>Navigates to the "Edit Sensor" page for a specific sensor.</summary>
    /// <param name="sensor">The sensor to edit.</param>
    Task NavigateToEditSensorAsync(Sensor sensor);

    /// <summary>Navigates to the sensor status summary page.</summary>
    Task NavigateToSensorStatusAsync();

    /// <summary>Navigates to the ticket creation page for a specific sensor.</summary>
    /// <param name="sensor">The sensor to create a ticket for.</param>
    Task NavigateToCreateTicketAsync(Sensor sensor);

    /// <summary>Navigates to the ticket details page. Restricted to Operations Managers.</summary>
    /// <param name="ticket">The ticket to view details for.</param>
    Task NavigateToTicketDetailsAsync(SensorTicket ticket);

    /// <summary>Navigates to the sensor reporting page.</summary>
    Task NavigateToSensorReportAsync();

    /// <summary>Navigates to the trend reporting page for a sensor.</summary>
    /// <param name="sensor">The sensor to view trend reports for.</param>
    Task NavigateToTrendReportAsync(Sensor sensor);

}

