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
            AverageValue = valuesList.Average(),          
            ModeValue = CalculateMode(valuesList),
            LatestValue = valuesList.LastOrDefault()
        };
    }
    
    protected static double CalculateMode(List<double> values)
    {
        if (values == null || !values.Any())
            return 0;
        
        return values
            .GroupBy(v => v)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault(defaultValue: values[0]);
    }
}
