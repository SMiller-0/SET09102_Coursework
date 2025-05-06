using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// Service that manages automatic refreshing of sensor data at regular intervals.
/// Implements ISensorRefreshService to provide sensor refresh functionality.
/// </summary>
public class SensorRefreshService : ISensorRefreshService
{
    // Dependencies injected through constructor
    private readonly ISensorService _sensorService;  // For retrieving sensor data
    private readonly ITimerService _timerService;    // For scheduling periodic refreshes

    /// <summary>
    /// Event that notifies subscribers when sensor data has been refreshed.
    /// Provides the updated collection of sensors to event handlers.
    /// </summary>
    public event EventHandler<IEnumerable<Sensor>>? SensorsRefreshed;

    /// <summary>
    /// Initializes a new instance of the SensorRefreshService class.
    /// </summary>
    /// <param name="sensorService">Service for accessing sensor data</param>
    /// <param name="timerService">Service for managing timed operations</param>
    public SensorRefreshService(ISensorService sensorService, ITimerService timerService)
    {
        _sensorService = sensorService;
        _timerService = timerService;
    }

    /// <summary>
    /// Begins automatic refreshing of sensor data at the specified interval.
    /// </summary>
    /// <param name="intervalSeconds">Time between refreshes in seconds</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task StartAutoRefresh(int intervalSeconds)
    {
        // Configure the timer to call RefreshSensors periodically
        _timerService.Start(TimeSpan.FromSeconds(intervalSeconds), 
            async () => await RefreshSensors());
    }

    /// <summary>
    /// Fetches the latest sensor data and notifies subscribers.
    /// Can be triggered manually or automatically by the timer.
    /// </summary>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task RefreshSensors()
    {
        try
        {
            // Retrieve all sensors (null parameter means no type filtering)
            var sensors = await _sensorService.GetSensorsByTypeAsync(null);
            
            // Notify subscribers with the updated sensor collection
            SensorsRefreshed?.Invoke(this, sensors);
        }
        catch (Exception ex)
        {
            // Log error but don't rethrow to prevent timer disruption
            Console.WriteLine($"Error refreshing sensors: {ex.Message}");
        }
    }

    /// <summary>
    /// Releases resources used by the service.
    /// Stops any active timers to prevent memory leaks.
    /// </summary>
    public void Dispose()
    {
        // Clean up the timer service
        _timerService.Dispose();
        
        // Prevent finalizer from running since we've explicitly disposed
        GC.SuppressFinalize(this);
    }
}
