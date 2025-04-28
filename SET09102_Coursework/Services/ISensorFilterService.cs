using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public interface ISensorFilterService
{
    IEnumerable<SensorStatusFilter> GetStatusFilterOptions();
    IEnumerable<SensorFilter> GetTypeFilterOptions(IEnumerable<SensorType> types);
    IEnumerable<Sensor> ApplyStatusFilter(IEnumerable<Sensor> sensors, SensorStatusFilter filter);
    IEnumerable<Sensor> ApplyTypeFilter(IEnumerable<Sensor> sensors, SensorFilter filter);
}

