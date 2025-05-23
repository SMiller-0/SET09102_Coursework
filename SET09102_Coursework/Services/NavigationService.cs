using Microsoft.Maui.Controls;   
using SET09102_Coursework.Models;
using SET09102_Coursework.Views;

namespace SET09102_Coursework.Services;

/// <inheritdoc/>
public class NavigationService : INavigationService
{
    readonly ICurrentUserService _user;

    public NavigationService(ICurrentUserService user)
    {
        _user = user;
    }

    private Dictionary<string, object> CreateSensorParameters(Sensor sensor) =>
        new() { { "sensor", sensor } };

    /// <inheritdoc/>
    public async Task NavigateToSensorDetailsAsync(Sensor sensor) =>
        await Shell.Current.GoToAsync(nameof(SensorPage), CreateSensorParameters(sensor));

    /// <inheritdoc/>
    public async Task NavigateToSensorSettingsAsync(Sensor sensor) =>
        await Shell.Current.GoToAsync(nameof(SensorSettingsPage), CreateSensorParameters(sensor));
 
    /// <inheritdoc/>
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
        
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(UpdateSettingsPage), parameters);
    }

    /// <inheritdoc/>
    public async Task NavigateToAddNewSensorAsync() =>
        await Shell.Current.GoToAsync(nameof(AddSensorPage));

    /// <inheritdoc/>
    public async Task NavigateToEditSensorAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(EditSensorPage), parameters);
    }

    /// <inheritdoc/>
    public async Task NavigateToSensorStatusAsync() =>
        await Shell.Current.GoToAsync(nameof(SensorStatusPage));

    /// <inheritdoc/>
    public async Task NavigateToCreateTicketAsync(Sensor sensor)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(CreateTicketPage), parameters);
    }

    /// <inheritdoc/>
    public async Task NavigateToTicketDetailsAsync(SensorTicket ticket)
    {
        if (!_user.IsOperationsManager)
        {
            await Shell.Current.DisplayAlert(
                "Access Denied",
                "Only Operations Managers can view ticket details.",
                "OK"
            );
            return;
        }

        await Shell.Current.GoToAsync(
            $"{nameof(TicketDetailsPage)}?ticketId={ticket.Id}"
        );
    }

    /// <inheritdoc/>
    public async Task NavigateToSensorReportAsync() =>
        await Shell.Current.GoToAsync(nameof(SensorReportPage));
    
    /// <inheritdoc/>
    public async Task NavigateToTrendReportAsync(Sensor sensor)
    {
        if (sensor == null) return;
        
        var parameters = new Dictionary<string, object>
        {
            { "sensor", sensor }
        };
        await Shell.Current.GoToAsync(nameof(TrendReportPage), parameters);
    }
}


