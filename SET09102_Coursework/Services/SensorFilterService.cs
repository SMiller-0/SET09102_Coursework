using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

/// <summary>
/// Service that provides filtering functionality for sensors.
/// Implements ISensorFilterService interface.
/// </summary>
public class SensorFilterService : ISensorFilterService
{
    /// <summary>
    /// Gets a collection of predefined status filter options for sensors.
    /// </summary>
    /// <returns>A collection of SensorStatusFilter objects for filtering by active status</returns>
    public IEnumerable<SensorStatusFilter> GetStatusFilterOptions()
    {
        return new List<SensorStatusFilter>
        {
            new() 
            { 
                DisplayName = "All Sensors",
                FilterPredicate = _ => true  // Always returns true to include all sensors
            },
            new() 
            { 
                DisplayName = "Active",
                FilterPredicate = s => s.IsActive  // Only includes active sensors
            },
            new() 
            { 
                DisplayName = "Inactive",
                FilterPredicate = s => !s.IsActive  // Only includes inactive sensors
            }
        };
    }

    /// <summary>
    /// Creates filter options for sensor types based on the provided collection of types.
    /// </summary>
    /// <param name="types">Collection of sensor types to create filters for</param>
    /// <returns>A collection of SensorFilter objects for filtering by sensor type</returns>
    public IEnumerable<SensorFilter> GetTypeFilterOptions(IEnumerable<SensorType> types)
    {
        var filters = new List<SensorFilter>
        {
            new() 
            { 
                SelectedTypeId = null,  // Null indicates no type filtering
                DisplayName = "All Sensors"
            }
        };

        foreach (var type in types)
        {
            filters.Add(new SensorFilter
            {
                SelectedTypeId = type.Id,
                DisplayName = type.Name
            });
        }

        return filters;
    }

    /// <summary>
    /// Applies a status filter to a collection of sensors.
    /// </summary>
    /// <param name="sensors">Collection of sensors to filter</param>
    /// <param name="filter">Status filter to apply</param>
    /// <returns>Filtered collection of sensors</returns>
    public IEnumerable<Sensor> ApplyStatusFilter(IEnumerable<Sensor> sensors, SensorStatusFilter filter)
    {
        if (sensors == null) return Enumerable.Empty<Sensor>();
        if (filter == null) return sensors;

        return sensors.Where(filter.FilterPredicate);
    }

    /// <summary>
    /// Applies a type filter to a collection of sensors.
    /// </summary>
    /// <param name="sensors">Collection of sensors to filter</param>
    /// <param name="filter">Type filter to apply</param>
    /// <returns>Filtered collection of sensors</returns>
    public IEnumerable<Sensor> ApplyTypeFilter(IEnumerable<Sensor> sensors, SensorFilter filter)
    {
        if (sensors == null) return Enumerable.Empty<Sensor>();
        if (filter == null) return sensors;
        if (!filter.SelectedTypeId.HasValue) return sensors;  // No filtering if type ID is null

        return sensors.Where(s => s.SensorTypeId == filter.SelectedTypeId.Value);
    }
}

