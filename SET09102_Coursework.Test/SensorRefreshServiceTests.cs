using Moq;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SET09102_Coursework.Test
{
    public class SensorRefreshServiceTests
    {
        /// <summary>
        /// Tests that refresh service raises event with sensors
        /// </summary>
        [Fact]
        public async Task RefreshSensors_RaisesEvent()
        {
            // Arrange
            var sensors = new List<Sensor> { new Sensor { Id = 100 } };
            var mockSensorService = new Mock<ISensorService>();
            mockSensorService.Setup(s => s.GetSensorsByTypeAsync(null))
                .ReturnsAsync(sensors);
                
            var mockTimerService = new Mock<ITimerService>();
            var refreshService = new SensorRefreshService(mockSensorService.Object, mockTimerService.Object);
            
            IEnumerable<Sensor> eventResult = null;
            refreshService.SensorsRefreshed += (sender, args) => eventResult = args;
            
            // Act
            await refreshService.RefreshSensors();
            
            // Assert
            Assert.NotNull(eventResult);
            Assert.Single(eventResult);
        }

        /// <summary>
        /// Tests that auto refresh starts timer
        /// </summary>
        [Fact]
        public async Task StartAutoRefresh_StartsTimer()
        {
            // Arrange
            var mockSensorService = new Mock<ISensorService>();
            var mockTimerService = new Mock<ITimerService>();
            var refreshService = new SensorRefreshService(mockSensorService.Object, mockTimerService.Object);
            
            // Act
            await refreshService.StartAutoRefresh(5);
            
            // Assert
            mockTimerService.Verify(t => t.Start(TimeSpan.FromSeconds(5), It.IsAny<Func<Task>>()), Times.Once);
        }
    }
}