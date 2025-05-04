using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

public abstract class BaseMeasurementStrategy
{
    protected readonly AppDbContext _context;
    
    protected BaseMeasurementStrategy(AppDbContext context)
    {
        _context = context;
    }
    
    protected MeasurementStatistic CalculateStatistic(IEnumerable<double> values, string parameterName)
    {
        var valuesList = values.ToList();
        if (!valuesList.Any())
        {
            return new MeasurementStatistic
            {
                ParameterName = parameterName,
                MaximumValue = 0,
                MinimumValue = 0,
                AverageValue = 0,
                ModeValue = 0,
                LatestValue = 0
            };
        }
        
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
    
    protected static double CalculateMode(List<double> values)
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