using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

/// <summary>
/// Implements the measurement strategy for weather sensors.
/// Handles the retrieval and statistical analysis of weather measurement data.
/// </summary>
public class WeatherMeasurementStrategy : BaseMeasurementStrategy, IMeasurementStrategy
{
    /// <summary>
    /// Gets the sensor type identifier for this strategy.
    /// </summary>
    public string SensorType => "Weather";

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherMeasurementStrategy"/> class.
    /// </summary>
    /// <param name="context">The database context for accessing measurement data.</param>
    public WeatherMeasurementStrategy(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves and calculates statistics for weather measurements from a specific sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor to retrieve measurements for.</param>
    /// <returns>A collection of measurement statistics for different weather parameters.</returns>
    public async Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId)
    {
        var statistics = new List<MeasurementStatistic>();
        
        // Retrieve all weather measurements for the specified sensor
        var weatherMeasurements = await _context.WeatherMeasurements
            .Where(m => m.SensorId == sensorId)
            .ToListAsync();
            
        if (weatherMeasurements.Any())
        {
            // Calculate statistics for each weather parameter
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.Temperature2m), "Temperature"));
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.RelativeHumidity2m), "Relative Humidity"));
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.WindSpeed10m), "Wind Speed"));
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.WindDirection10m), "Wind Direction"));
        }
        
        return statistics;
    }
}
