using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

/// <summary>
/// Implements the measurement strategy for water quality sensors.
/// Handles the retrieval and statistical analysis of water measurement data.
/// </summary>
public class WaterMeasurementStrategy : BaseMeasurementStrategy, IMeasurementStrategy
{
    /// <summary>
    /// Gets the sensor type identifier for this strategy.
    /// </summary>
    public string SensorType => "Water";

    /// <summary>
    /// Initializes a new instance of the <see cref="WaterMeasurementStrategy"/> class.
    /// </summary>
    /// <param name="context">The database context for accessing measurement data.</param>
    public WaterMeasurementStrategy(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves and calculates statistics for water quality measurements from a specific sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor to retrieve measurements for.</param>
    /// <returns>A collection of measurement statistics for different water quality parameters.</returns>
    public async Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId)
    {
        var statistics = new List<MeasurementStatistic>();
        
        // Retrieve all water measurements for the specified sensor
        var waterMeasurements = await _context.WaterMeasurements
            .Where(m => m.SensorId == sensorId)
            .ToListAsync();
            
        if (waterMeasurements.Any())
        {
            // Calculate statistics for each water quality parameter
            statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Nitrate), "Nitrate"));
            statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Nitrite), "Nitrite"));
            statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Phosphate), "Phosphate"));
        }
        
        return statistics;
    }
}
