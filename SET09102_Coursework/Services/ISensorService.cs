using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public interface ISensorService
{
    Task<IEnumerable<SensorType>> GetSensorTypesAsync();
    Task<IEnumerable<Sensor>> GetSensorsByTypeAsync(int? typeId);
    Task<bool> UpdateFirmwareVersionAsync(int sensorId, string newVersion);
    Task<IEnumerable<Settings>> GetSensorSettingsAsync(int sensorId);
    Task<bool> UpdateSensorSettingsAsync(IEnumerable<Settings> settings);
    Task<bool> AddSensorAsync(Sensor sensor);
}   


