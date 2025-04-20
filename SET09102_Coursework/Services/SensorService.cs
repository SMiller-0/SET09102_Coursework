using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public class SensorService : ISensorService
{
    private readonly AppDbContext _context;

    public SensorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SensorType>> GetSensorTypesAsync()
    {
        return await _context.SensorTypes.OrderBy(st => st.Name).ToListAsync();
    }

    public async Task<IEnumerable<Sensor>> GetSensorsByTypeAsync(int? typeId)
    {
        try
        {
            var query = _context.Sensors
                .Include(s => s.SensorType)
                .OrderBy(s => s.Name)
                .AsQueryable();

            if (typeId.HasValue)
            {
                query = query.Where(s => s.SensorTypeId == typeId.Value);
            }

            return await query
                .OrderByDescending(s => s.IsActive)
                .ThenBy(s => s.Name)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving sensors: {ex.Message}");
            return Enumerable.Empty<Sensor>();
        }
    }

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

    public async Task<IEnumerable<Settings>> GetSensorSettingsAsync(int sensorId)
    {
        return await _context.Settings
            .Include(s => s.SettingType)
            .Where(s => s.SensorId == sensorId)
            .OrderBy(s => s.SettingType.Name)
            .ToListAsync();
    }

    public async Task<bool> UpdateSensorSettingsAsync(IEnumerable<Settings> settings)
    {
        try
        {
            foreach (var setting in settings)
            {
                var existingSetting = await _context.Settings.FindAsync(setting.Id);
                if (existingSetting != null)
                {
                    existingSetting.MinimumValue = setting.MinimumValue;
                    existingSetting.MaximumValue = setting.MaximumValue;
                }
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
}


