using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public class AirMeasurementStrategy : BaseMeasurementStrategy, IMeasurementStrategy
{
    public string SensorType => "Air";
    
    public AirMeasurementStrategy(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId)
    {
        var statistics = new List<MeasurementStatistic>();
        
        var airMeasurements = await _context.AirMeasurements
            .Where(m => m.SensorId == sensorId)
            .ToListAsync();
            
        if (airMeasurements.Any())
        {
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.NitrogenDioxide), "Nitrogen Dioxide"));
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.SulphurDioxide), "Sulphur Dioxide"));
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.PM25), "PM2.5"));
            statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.PM10), "PM10"));
        }
        
        return statistics;
    }
}
