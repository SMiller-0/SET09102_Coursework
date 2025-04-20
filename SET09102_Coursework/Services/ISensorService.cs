using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// Defines sensor-related data operations.
/// </summary>
public interface ISensorService
{
    Task<IEnumerable<SensorType>> GetSensorTypesAsync();
    /// <summary>
    /// Retrieves all sensors, optionally filtered by sensor type ID.
    /// Active sensors are listed first.
    /// </summary>
    /// <param name="typeId">Optional sensor type ID to filter by.</param>
    /// <returns>A list of matching Sensor objects.</returns> 
    Task<IEnumerable<Sensor>> GetSensorsByTypeAsync(int? typeId);
    Task<bool> UpdateFirmwareVersionAsync(int sensorId, string newVersion);
    Task<IEnumerable<Settings>> GetSensorSettingsAsync(int sensorId);
    Task<bool> UpdateSensorSettingsAsync(IEnumerable<Settings> settings);

    /// <summary>
    /// Adds a new sensor to the database.
    /// </summary>
    /// <param name="sensor">The sensor to add.</param>
    /// <returns>True if saved successfully; otherwise, false.</returns>
    Task<bool> AddSensorAsync(Sensor sensor);
}   


