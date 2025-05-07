using Moq;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SET09102_Coursework.Test
{
    public class TrendReportViewModelTests
    {
        /// <summary>
        /// Tests that view model loads statistics for a valid sensor
        /// </summary>
        [Fact]
        public async Task LoadStatistics_PopulatesCollection()
        {
            // Arrange
            var sensor = new Sensor { Id = 100, Name = "Test Sensor" };
            var stats = new List<MeasurementStatistic> 
            { 
                new MeasurementStatistic { ParameterName = "Temperature" } 
            };
            
            var mockService = new Mock<IReportService>();
            mockService.Setup(r => r.GenerateTrendReportAsync(sensor))
                .ReturnsAsync(stats);
                
            var viewModel = new TrendReportViewModel(mockService.Object);
            viewModel.Sensor = sensor;
            
            // Act
            await viewModel.LoadStatisticsCommand.ExecuteAsync(null);
            
            // Assert
            Assert.Single(viewModel.Statistics);
            Assert.Equal("Temperature", viewModel.Statistics[0].ParameterName);
        }

        /// <summary>
        /// Tests that view model handles null sensor correctly
        /// </summary>
        [Fact]
        public async Task NullSensor_DoesNothing()
        {
            // Arrange
            var mockService = new Mock<IReportService>();
            var viewModel = new TrendReportViewModel(mockService.Object);
            viewModel.Sensor = null;
            
            // Act
            await viewModel.LoadStatisticsCommand.ExecuteAsync(null);
            
            // Assert
            Assert.Empty(viewModel.Statistics);
        }

        /// <summary>
        /// Tests that view model sets sensor from query parameters
        /// </summary>
        [Fact]
        public void QueryAttributes_SetsSensor()
        {
            // Arrange
            var sensor = new Sensor { Id = 147, Name = "Water Sensor" };
            var mockService = new Mock<IReportService>();
            var viewModel = new TrendReportViewModel(mockService.Object);
            
            // Act
            ((IQueryAttributable)viewModel).ApplyQueryAttributes(
                new Dictionary<string, object> { { "sensor", sensor } });
            
            // Assert
            Assert.Equal(sensor, viewModel.Sensor);
            Assert.Contains(sensor.Name, viewModel.ReportTitle);
        }
    }
}