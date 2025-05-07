using Moq;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SET09102_Coursework.Test
{
    public class ReportServiceTests
    {
        /// <summary>
        /// Tests that report service returns statistics for a valid sensor
        /// </summary>
        [Fact]
        public async Task ValidSensor_ReturnsStatistics()
        {
            // Arrange
            var sensor = new Sensor { Id = 100, SensorType = new SensorType { Name = "Weather" } };
            var mockService = new Mock<IMeasurementService>();
            mockService.Setup(m => m.GetSensorStatisticsAsync(100, "weather"))
                .ReturnsAsync(new List<MeasurementStatistic> { new MeasurementStatistic() });
            
            var reportService = new ReportService(mockService.Object);
            
            // Act
            var result = await reportService.GenerateTrendReportAsync(sensor);
            
            // Assert
            Assert.Single(result);
        }

        /// <summary>
        /// Tests that report service returns empty collection for null sensor
        /// </summary>
        [Fact]
        public async Task NullSensor_ReturnsEmptyCollection()
        {
            // Arrange
            var mockService = new Mock<IMeasurementService>();
            var reportService = new ReportService(mockService.Object);
            
            // Act
            var result = await reportService.GenerateTrendReportAsync(null);
            
            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests that report service returns empty collection when no data available
        /// </summary>
        [Fact]
        public async Task NoData_ReturnsEmptyCollection()
        {
            // Arrange
            var sensor = new Sensor { Id = 243, SensorType = new SensorType { Name = "Air" } };
            var mockService = new Mock<IMeasurementService>();
            mockService.Setup(m => m.GetSensorStatisticsAsync(243, "air"))
                .ReturnsAsync(new List<MeasurementStatistic>());
            
            var reportService = new ReportService(mockService.Object);
            
            // Act
            var result = await reportService.GenerateTrendReportAsync(sensor);
            
            // Assert
            Assert.Empty(result);
        }
    }
}