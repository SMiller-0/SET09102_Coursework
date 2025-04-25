using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public class SensorRefreshService : ISensorRefreshService
{
    private readonly ISensorService _sensorService;
    private readonly ITimerService _timerService;

    public event EventHandler<IEnumerable<Sensor>>? SensorsRefreshed;

    public SensorRefreshService(ISensorService sensorService, ITimerService timerService)
    {
        _sensorService = sensorService;
        _timerService = timerService;
    }

    public async Task StartAutoRefresh(int intervalSeconds)
    {
        _timerService.Start(TimeSpan.FromSeconds(intervalSeconds), 
            async () => await RefreshSensors());
    }

    public async Task RefreshSensors()
    {
        try
        {
            var sensors = await _sensorService.GetSensorsByTypeAsync(null);
            SensorsRefreshed?.Invoke(this, sensors);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error refreshing sensors: {ex.Message}");
        }
    }

    public void Dispose()
    {
        _timerService.Dispose();
        GC.SuppressFinalize(this);
    }
}
