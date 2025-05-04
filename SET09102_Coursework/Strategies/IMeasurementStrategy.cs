using SET09102_Coursework.Models;

namespace SET09102_Coursework.Strategies;

public interface IMeasurementStrategy
{
    string SensorType { get; }
    Task<IEnumerable<MeasurementStatistic>> GetStatisticsAsync(int sensorId);
}
