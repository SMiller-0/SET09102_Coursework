using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

/// <summary>
/// Provides a base implementation for measurement strategies.
/// Contains common functionality for calculating statistics from measurement data.
/// </summary>
public abstract class BaseMeasurementStrategy
{
    /// <summary>
    /// The database context used to access measurement data.
    /// </summary>
    protected readonly AppDbContext _context;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseMeasurementStrategy"/> class.
    /// </summary>
    /// <param name="context">The database context for accessing measurement data.</param>
    protected BaseMeasurementStrategy(AppDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Calculates statistical values from a collection of measurement values.
    /// </summary>
    /// <param name="values">The collection of measurement values to analyze.</param>
    /// <param name="parameterName">The name of the parameter being measured.</param>
    /// <returns>A <see cref="MeasurementStatistic"/> object containing calculated statistics.</returns>
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
    
    /// <summary>
    /// Calculates the mode (most frequently occurring value) from a list of values.
    /// </summary>
    /// <param name="values">The list of values to analyze.</param>
    /// <returns>
    /// The most frequently occurring value in the list.
    /// If multiple values have the same highest frequency, returns the first one found.
    /// If the list is empty or null, returns 0.
    /// </returns>
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

