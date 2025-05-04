using SET09102_Coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SET09102_Coursework.Services;

public class ReportService : IReportService
{
    private readonly IMeasurementService _measurementService;

    public ReportService(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    public async Task<IEnumerable<MeasurementStatistic>> GenerateTrendReportAsync(Sensor sensor)
    {
        if (sensor == null)
            return Enumerable.Empty<MeasurementStatistic>();

        var sensorType = sensor.SensorType.Name.ToLower();
        return await _measurementService.GetSensorStatisticsAsync(sensor.Id, sensorType);
    }
}