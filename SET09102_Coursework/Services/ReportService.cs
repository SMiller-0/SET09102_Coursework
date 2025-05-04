using SET09102_Coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SET09102_Coursework.Services;

/// <summary>
/// Service for generating reports based on sensor data.
/// </summary>
public class ReportService : IReportService
{
    private readonly IMeasurementService _measurementService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportService"/> class.
    /// </summary>
    /// <param name="measurementService">The measurement service used to retrieve sensor statistics.</param>
    public ReportService(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    /// <summary>
    /// Generates a trend report for a specific sensor.
    /// </summary>
    /// <param name="sensor">The sensor to generate the report for.</param>
    /// <returns>A collection of measurement statistics for the sensor.</returns>
    public async Task<IEnumerable<MeasurementStatistic>> GenerateTrendReportAsync(Sensor sensor)
    {
        if (sensor == null)
            return Enumerable.Empty<MeasurementStatistic>();

        var sensorType = sensor.SensorType.Name.ToLower();
        return await _measurementService.GetSensorStatisticsAsync(sensor.Id, sensorType);
    }
}
