using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SET09102_Coursework.Test
{
    public class SensorFilterServiceTests
    {
        /// <summary>
        /// Tests that status filter options are created correctly
        /// </summary>
        [Fact]
        public void GetStatusFilterOptions_ReturnsFilters()
        {
            // Arrange
            var filterService = new SensorFilterService();
            
            // Act
            var filters = filterService.GetStatusFilterOptions().ToList();
            
            // Assert
            Assert.Equal(3, filters.Count);
            Assert.Equal("All Sensors", filters[0].DisplayName);
        }

        /// <summary>
        /// Tests that active filter returns only active sensors
        /// </summary>
        [Fact]
        public void ActiveFilter_ReturnsActiveSensors()
        {
            // Arrange
            var sensors = new List<Sensor>
            {
                new Sensor { Name = "Active Sensor", IsActive = true },
                new Sensor { Name = "Inactive Sensor", IsActive = false }
            };
            
            var filterService = new SensorFilterService();
            var activeFilter = filterService.GetStatusFilterOptions().ElementAt(1); // "Active" filter
            
            // Act
            var result = filterService.ApplyStatusFilter(sensors, activeFilter).ToList();
            
            // Assert
            Assert.Single(result);
            Assert.True(result[0].IsActive);
        }

        /// <summary>
        /// Tests that all sensors filter returns all sensors
        /// </summary>
        [Fact]
        public void AllSensorsFilter_ReturnsAllSensors()
        {
            // Arrange
            var sensors = new List<Sensor>
            {
                new Sensor { IsActive = true },
                new Sensor { IsActive = false }
            };
            
            var filterService = new SensorFilterService();
            var allFilter = filterService.GetStatusFilterOptions().ElementAt(0); // "All Sensors" filter
            
            // Act
            var result = filterService.ApplyStatusFilter(sensors, allFilter).ToList();
            
            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}