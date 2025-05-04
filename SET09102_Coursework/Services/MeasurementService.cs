
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Strategies;

namespace SET09102_Coursework.Services;

public class MeasurementService : IMeasurementService
{    
    private readonly AppDbContext _context;
    private readonly IEnumerable<IMeasurementStrategy> _strategies;

    public MeasurementService(
        AppDbContext context,
        IEnumerable<IMeasurementStrategy> strategies)
    {        
        _context = context;
        _strategies = strategies;
    }
    
    public async Task<IEnumerable<MeasurementStatistic>> GetSensorStatisticsAsync(int sensorId, string sensorType)    
    {
        if (string.IsNullOrEmpty(sensorType))
            return Enumerable.Empty<MeasurementStatistic>();
        
        var strategy = _strategies.FirstOrDefault(s => 
            s.SensorType.Equals(sensorType, StringComparison.OrdinalIgnoreCase));
        
        if (strategy == null)
            return Enumerable.Empty<MeasurementStatistic>();
        
        return await strategy.GetStatisticsAsync(sensorId);
    }
}
