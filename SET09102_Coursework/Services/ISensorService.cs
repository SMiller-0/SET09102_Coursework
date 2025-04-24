using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// This interface provides methods to manage sensors, including adding, updating, deleting, and retrieving sensor information.
/// </summary>
public interface ISensorService
{
    /// <summary>
    /// Retrieves all available sensor types from the database.
    /// </summary>
    /// <returns>A collection of SensorType objects representing all sensor types in the system.</returns>
    /// <remarks>
    /// Returns an empty collection if no sensor types are found.
    /// The sensor types are used for categorizing and filtering sensors.
    /// </remarks>
    Task<IEnumerable<SensorType>> GetSensorTypesAsync();
    
    /// <summary>
    /// Retrieves all sensors, optionally filtered by sensor type ID.
    /// Active sensors are listed first.
    /// </summary>
    /// <param name="typeId">Optional sensor type ID to filter by.</param>
    /// <returns>A list of matching Sensor objects.</returns> 

    Task<IEnumerable<Sensor>> GetSensorsByTypeAsync(int? typeId);
    /// <summary>
    /// Updates the firmware version of a specified sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor to update.</param>
    /// <param name="newVersion">The new firmware version to set.</param>
    /// <returns>True if the firmware version was updated successfully; otherwise, false.</returns>
    /// <remarks>
    /// The firmware version should follow semantic versioning format (X.Y.Z).
    /// </remarks>

    Task<bool> UpdateFirmwareVersionAsync(int sensorId, string newVersion);

    /// <summary>
    /// Retrieves all settings associated with a specific sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor whose settings are to be retrieved.</param>
    /// <returns>A collection of Settings objects associated with the specified sensor.</returns>
    /// <remarks>
    /// Returns an empty collection if no settings are found or if the sensor doesn't exist.
    /// </remarks>

    Task<IEnumerable<Settings>> GetSensorSettingsAsync(int sensorId);

    /// <summary>
    /// Updates multiple settings for a sensor simultaneously.
    /// </summary>
    /// <param name="settings">A collection of Settings objects to be updated.</param>
    /// <returns>True if all settings were updated successfully; otherwise, false.</returns>
    /// <remarks>
    /// This method should validate that all settings are valid before applying any updates.
    /// If any setting update fails, the entire operation should be rolled back.
    /// </remarks>
    
    Task<bool> UpdateSensorSettingsAsync(IEnumerable<Settings> settings);

    /// <summary>
    /// Adds a new sensor to the database.
    /// </summary>
    /// <param name="sensor">The sensor to add.</param>
    /// <returns>True if saved successfully; otherwise, false.</returns>
    Task<bool> AddSensorAsync(Sensor sensor);

    /// <summary>
    /// Updates an existing sensor in the database.
    /// </summary>
    /// <param name="sensor">The sensor to update.</param>
    /// <returns>True if updated successfully; otherwise, false.</returns>
    Task<bool> UpdateSensorAsync(Sensor sensor);

    /// <summary>
    /// Deletes a sensor from the database.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor to delete.</param>
    /// <returns>True if deleted successfully; otherwise, false.</returns>
    Task<bool> DeleteSensorAsync(int sensorId);

}   


