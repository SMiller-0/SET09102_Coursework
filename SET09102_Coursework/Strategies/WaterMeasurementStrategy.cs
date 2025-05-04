using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

public class WaterMeasurementStrategy : BaseMeasurementStrategy, IMeasurementStrategy
{
    public string SensorType => "Water";
    public WaterMeasurementStrategy(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId)
    {
        var statistics = new List<MeasurementStatistic>();
        
        var waterMeasurements = await _context.WaterMeasurements
            .Where(m => m.SensorId == sensorId)
            .ToListAsync();
            
        if (waterMeasurements.Any())
        {
            statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Nitrate), "Nitrate"));
            statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Nitrite), "Nitrite"));
            statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Phosphate), "Phosphate"));
        }
        
        return statistics;
    }
}