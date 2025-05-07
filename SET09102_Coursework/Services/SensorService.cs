using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// Service that provides CRUD operations for sensors and their related data.
/// Implements ISensorService interface.
/// </summary>
public class SensorService : ISensorService
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the SensorService class.
    /// </summary>
    /// <param name="context">Database context for accessing sensor data</param>
    public SensorService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all sensor types from the database, ordered by name.
    /// </summary>
    /// <returns>A collection of sensor types</returns>
    public async Task<IEnumerable<SensorType>> GetSensorTypesAsync()
    {
        return await _context.SensorTypes.OrderBy(st => st.Name).ToListAsync();
    }

    /// <summary>
    /// Retrieves sensors from the database, optionally filtered by type.
    /// Orders results with active sensors first, then by name.
    /// </summary>
    /// <param name="typeId">Optional sensor type ID to filter by</param>
    /// <returns>A collection of sensors matching the criteria</returns>
    public async Task<IEnumerable<Sensor>> GetSensorsByTypeAsync(int? typeId)
    {
        try
        {
            _context.ChangeTracker.Clear();
            
            var query = _context.Sensors
                .Include(s => s.SensorType)
                .AsNoTracking()
                .OrderByDescending(s => s.IsActive)
                .ThenBy(s => s.Name);

            return typeId.HasValue ? 
                await query.Where(s => s.SensorTypeId == typeId.Value).ToListAsync() : 
                await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving sensors: {ex.Message}");
            return Enumerable.Empty<Sensor>();
        }
    }

    /// <summary>
    /// Updates the firmware version of a specific sensor.
    /// </summary>
    /// <param name="sensorId">ID of the sensor to update</param>
    /// <param name="newVersion">New firmware version string</param>
    /// <returns>True if update was successful, false otherwise</returns>
    public async Task<bool> UpdateFirmwareVersionAsync(int sensorId, string newVersion)
    {
        try
        {
            var sensor = await _context.Sensors.FindAsync(sensorId);
            if (sensor == null) return false;

            sensor.FirmwareVersion = newVersion;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Retrieves all settings for a specific sensor, including setting types.
    /// </summary>
    /// <param name="sensorId">ID of the sensor</param>
    /// <returns>Collection of settings for the specified sensor</returns>
    public async Task<IEnumerable<Settings>> GetSensorSettingsAsync(int sensorId)
    {
        return await _context.Settings
            .Include(s => s.SettingType)
            .Where(s => s.SensorId == sensorId)
            .OrderBy(s => s.SettingType.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Updates multiple settings for a sensor in a single transaction.
    /// </summary>
    /// <param name="settings">Collection of settings to update</param>
    /// <returns>True if all updates were successful, false otherwise</returns>
    public async Task<bool> UpdateSensorSettingsAsync(IEnumerable<Settings> settings)
    {
        try
        {
            var existingSettings = await _context.Settings
                .Where(s => settings.Select(x => x.Id).Contains(s.Id))
                .ToListAsync();

            foreach (var setting in existingSettings)
            {
                var newSetting = settings.First(s => s.Id == setting.Id);
                setting.MinimumValue = newSetting.MinimumValue;
                setting.MaximumValue = newSetting.MaximumValue;
            }
            
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> AddSensorAsync(Sensor sensor)
    {
        try
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save sensor: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateSensorAsync(Sensor sensor)
    {
        try
        {
            _context.ChangeTracker.Clear(); 
        
            var existingSensor = await _context.Sensors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == sensor.Id);
            
            if (existingSensor == null) return false;

            _context.Entry(sensor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating sensor: {ex.Message}");
            return false;
        }
    }


    public async Task<bool> DeleteSensorAsync(int sensorId)
    {
        try
        {
            var sensor = await _context.Sensors.FindAsync(sensorId);
            if (sensor == null) return false;

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting sensor: {ex.Message}");
            return false;
        }
    }
}
