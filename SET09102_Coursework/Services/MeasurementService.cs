
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Strategies;

namespace SET09102_Coursework.Services;

/// <summary>
/// Service for retrieving and processing measurement data from sensors.
/// </summary>
public class MeasurementService : IMeasurementService
{    
    private readonly AppDbContext _context;
    private readonly IEnumerable<IMeasurementStrategy> _strategies;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="strategies">The collection of measurement strategies.</param>
    public MeasurementService(
        AppDbContext context,
        IEnumerable<IMeasurementStrategy> strategies)
    {        
        _context = context;
        _strategies = strategies;
    }
    
    /// <summary>
    /// Gets statistics for a specific sensor based on its type.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor.</param>
    /// <param name="sensorType">The type of the sensor.</param>
    /// <returns>A collection of measurement statistics for the sensor.</returns>
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
