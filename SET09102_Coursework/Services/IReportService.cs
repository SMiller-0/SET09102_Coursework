using SET09102_Coursework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SET09102_Coursework.Services;

public interface IReportService
{
    Task<IEnumerable<MeasurementStatistic>> GenerateTrendReportAsync(Sensor sensor);
}