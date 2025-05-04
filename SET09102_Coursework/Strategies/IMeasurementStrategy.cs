using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

/// <summary>
/// Defines the interface for measurement strategies in the application.
/// </summary>
public interface IMeasurementStrategy
{
    /// <summary>
    /// Gets the type of sensor this strategy handles.
    /// </summary>
    string SensorType { get; }
    
    /// <summary>
    /// Retrieves and calculates statistics for measurements from a specific sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor to retrieve measurements for.</param>
    /// <returns>A collection of measurement statistics for various parameters measured by the sensor.</returns>
    Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId);
}

