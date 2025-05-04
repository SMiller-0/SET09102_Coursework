using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;


namespace SET09102_Coursework.Services;

public class MeasurementService : IMeasurementService
{    
    private readonly AppDbContext _context;
    private readonly Dictionary<string, IMeasurementStrategy> _strategies;
    
    public MeasurementService(AppDbContext context)
    {        
        _context = context;
        _strategies = new Dictionary<string, IMeasurementStrategy>(StringComparer.OrdinalIgnoreCase)
        {
            { "air", new AirMeasurementStrategy(_context) },
            { "water", new WaterMeasurementStrategy(_context) },
            { "weather", new WeatherMeasurementStrategy(_context) }
        };
    }
    
    public async Task<IEnumerable<MeasurementStatistic>> GetSensorStatisticsAsync(int sensorId, string sensorType)    
    {
        if (string.IsNullOrEmpty(sensorType) || !_strategies.TryGetValue(sensorType, out var strategy))
        {
            return Enumerable.Empty<MeasurementStatistic>();
        }
        
        return await strategy.GetStatisticsAsync(sensorId);
    }
}
