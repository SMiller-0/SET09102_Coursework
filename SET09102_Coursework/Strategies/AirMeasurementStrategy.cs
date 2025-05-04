using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

/// <summary>
/// Implements the measurement strategy for air quality sensors.
/// Handles the retrieval and statistical analysis of air measurement data.
/// </summary>
public class AirMeasurementStrategy : BaseMeasurementStrategy, IMeasurementStrategy
{
    /// <summary>
    /// Gets the sensor type identifier for this strategy.
    /// </summary>
    public string SensorType => "Air";
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AirMeasurementStrategy"/> class.
    /// </summary>
    /// <param name="context">The database context for accessing measurement data.</param>
    public AirMeasurementStrategy(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves and calculates statistics for air quality measurements from a specific sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor to retrieve measurements for.</param>
    /// <returns>A collection of measurement statistics for different air quality parameters.</returns>
    public async Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId)
    {
        var statistics = new List<MeasurementStatistic>();
        
        // Retrieve all air measurements for the specified sensor
        var airMeasurements = await _context.AirMeasurements
            .Where(m => m.SensorId == sensorId)
            .ToListAsync();
            
        if (airMeasurements.Any())
        {
            // Calculate statistics for each air quality parameter
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.NitrogenDioxide), "Nitrogen Dioxide"));
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.SulphurDioxide), "Sulphur Dioxide"));
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.PM25), "PM2.5"));
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.PM10), "PM10"));
        }
        
        return statistics;
    }
}

