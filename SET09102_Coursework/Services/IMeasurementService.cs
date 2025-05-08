using SET09102_Coursework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SET09102_Coursework.Services;

public interface IMeasurementService
{
    Task<IEnumerable<MeasurementStatistic>> GetSensorStatisticsAsync(int sensorId, string sensorType);
}