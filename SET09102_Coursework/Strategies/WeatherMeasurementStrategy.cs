using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public class WeatherMeasurementStrategy : BaseMeasurementStrategy, IMeasurementStrategy
{
    public WeatherMeasurementStrategy(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId)
    {
        var statistics = new List<MeasurementStatistic>();
        
        var weatherMeasurements = await _context.WeatherMeasurements
            .Where(m => m.SensorId == sensorId)
            .ToListAsync();
            
        if (weatherMeasurements.Any())
        {
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.Temperature2m), "Temperature"));
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.RelativeHumidity2m), "Relative Humidity"));
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.WindSpeed10m), "Wind Speed"));
            statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.WindDirection10m), "Wind Direction"));
        }
        
        return statistics;
    }
}