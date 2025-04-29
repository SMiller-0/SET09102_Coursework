using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public class SensorFilterService : ISensorFilterService
{
    public IEnumerable<SensorStatusFilter> GetStatusFilterOptions()
    {
        return new List<SensorStatusFilter>
        {
            new() 
            { 
                DisplayName = "All Sensors",
                FilterPredicate = _ => true
            },
            new() 
            { 
                DisplayName = "Active",
                FilterPredicate = s => s.IsActive
            },
            new() 
            { 
                DisplayName = "Inactive",
                FilterPredicate = s => !s.IsActive
            }
        };
    }

    public IEnumerable<SensorFilter> GetTypeFilterOptions(IEnumerable<SensorType> types)
    {
        var filters = new List<SensorFilter>
        {
            new() 
            { 
                SelectedTypeId = null,
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

    public IEnumerable<Sensor> ApplyStatusFilter(IEnumerable<Sensor> sensors, SensorStatusFilter filter)
    {
        if (sensors == null) return Enumerable.Empty<Sensor>();
        if (filter == null) return sensors;

        return sensors.Where(filter.FilterPredicate);
    }

    public IEnumerable<Sensor> ApplyTypeFilter(IEnumerable<Sensor> sensors, SensorFilter filter)
    {
        if (sensors == null) return Enumerable.Empty<Sensor>();
        if (filter == null) return sensors;
        if (!filter.SelectedTypeId.HasValue) return sensors;

        return sensors.Where(s => s.SensorTypeId == filter.SelectedTypeId.Value);
    }
}

