using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;using SET09102_Coursework.Models;
using System;using System.Collections.Generic;
using System.Linq;using System.Threading.Tasks;
namespace SET09102_Coursework.Services;
public class MeasurementService : IMeasurementService
{    
    private readonly AppDbContext _context;
    public MeasurementService(AppDbContext context)
    {        
        _context = context;
    }
    
    public async Task<IEnumerable<MeasurementStatistic>> GetSensorStatisticsAsync(int sensorId, string sensorType)    
    {
        var statistics = new List<MeasurementStatistic>();
        switch (sensorType.ToLower())        
        {
            case "air":                
                var airMeasurements = await _context.AirMeasurements
                .Where(m => m.SensorId == sensorId)                    
                .ToListAsync();
                if (airMeasurements.Any())
                {   statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.NitrogenDioxide), "Nitrogen Dioxide"));
                    statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.SulphurDioxide), "Sulphur Dioxide"));                    
                    statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.PM25), "PM2.5"));
                    statistics.Add(CalculateStatistic(airMeasurements.Select(m => m.PM10), "PM10"));                
                }
                break;
            case "water":                
                var waterMeasurements = await _context.WaterMeasurements
                .Where(m => m.SensorId == sensorId)                    
                .ToListAsync();
                if (waterMeasurements.Any())
                {   statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Nitrate), "Nitrate"));
                    statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Nitrite), "Nitrite"));                    
                    statistics.Add(CalculateStatistic(waterMeasurements.Select(m => m.Phosphate), "Phosphate"));
                }                
                break;
            case "weather":
                var weatherMeasurements = await _context.WeatherMeasurements                    
                .Where(m => m.SensorId == sensorId)
                .ToListAsync();
                if (weatherMeasurements.Any())                
                {
                    statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.Temperature2m), "Temperature"));                    
                    statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.RelativeHumidity2m), "Humidity"));
                    statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.WindSpeed10m), "Wind Speed"));                    
                    statistics.Add(CalculateStatistic(weatherMeasurements.Select(m => m.WindDirection10m), "Wind Direction"));
                }               break;
        }
        return statistics;    
    }

    private MeasurementStatistic CalculateStatistic(IEnumerable<double> values, string parameterName)
    {        
        var valuesList = values.ToList();
        return new MeasurementStatistic
        {   
            ParameterName = parameterName,
            MaximumValue = valuesList.Max(),            
            MinimumValue = valuesList.Min(),
            AverageValue = Math.Round(valuesList.Average(), 2),            
            ModeValue = CalculateMode(valuesList),
            LatestValue = valuesList.LastOrDefault()
        };    
    }

    private static double CalculateMode(List<double> values)
    {
        if (values == null || !values.Any())
        {
            return 0;
        }

        var roundedValues = values.Select(v => Math.Round(v, 2));
        
        var groupedValues = roundedValues
            .GroupBy(v => v)
            .OrderByDescending(g => g.Count());
        
        return groupedValues.Any() ? groupedValues.First().Key : values[0];
    }
}


















































