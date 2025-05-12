using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="SensorService"/>.
    /// Verifies that the service can update and delete sensors correctly.
    /// </summary>
    public class SensorServiceTests
    {
        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
            => new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        /// <summary>
        /// UpdateSensorAsync should return true and persist changes when the sensor exists.
        /// </summary>
        [Fact]
        public async Task UpdateSensorAsync_ExistingSensor_ReturnsTrueAndUpdates()
        {
            // Arrange: seed one sensor
            var opts = CreateNewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.Sensors.Add(new Sensor { Id = 7, Name = "OldName", IsActive = true, SensorTypeId = 0, FirmwareVersion = "fv" });
                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new SensorService(ctx);

            // Act: attempt to update the existing sensor
            var modified = new Sensor { Id = 7, Name = "NewName", IsActive = false, SensorTypeId = 0, FirmwareVersion = "fv" };
            var ok = await svc.UpdateSensorAsync(modified);

            // Assert: update returns true and persisted values match
            Assert.True(ok);
            var reloaded = await ctx.Sensors.FindAsync(7);
            Assert.Equal("NewName", reloaded.Name);
            Assert.False(reloaded.IsActive);
        }

        /// <summary>
        /// UpdateSensorAsync should return false when the sensor does not exist.
        /// </summary>
        [Fact]
        public async Task UpdateSensorAsync_Nonexistent_ReturnsFalse()
        {
            // Arrange: empty database
            var opts = CreateNewContextOptions();
            using var ctx = new AppDbContext(opts);
            var svc = new SensorService(ctx);

            // Act: attempt to update a non-existent sensor
            var bogus = new Sensor { Id = 1234, Name = "X", IsActive = true, SensorTypeId = 0, FirmwareVersion = "" };
            Assert.False(await svc.UpdateSensorAsync(bogus));
        }

        /// <summary>
        /// DeleteSensorAsync should return true and remove the sensor when it exists.
        /// </summary>
        [Fact]
        public async Task DeleteSensorAsync_Existing_ReturnsTrueAndRemoves()
        {
            // Arrange: seed one sensor
            var opts = CreateNewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.Sensors.Add(new Sensor { Id = 55, Name = "ToDelete", IsActive = true, SensorTypeId = 0, FirmwareVersion = "" });
                await seed.SaveChangesAsync();
            }

            // Act: attempt to delete the existing sensor
            using var ctx = new AppDbContext(opts);
            var svc = new SensorService(ctx);

            // Assert: returns true and sensor no longer found
            Assert.True(await svc.DeleteSensorAsync(55));
            Assert.Null(await ctx.Sensors.FindAsync(55));
        }

        /// <summary>
        /// DeleteSensorAsync should return false when the sensor does not exist.
        /// </summary>
        [Fact]
        public async Task DeleteSensorAsync_Nonexistent_ReturnsFalse()
        {
            // Arrange: empty database
            var opts = CreateNewContextOptions();
            using var ctx = new AppDbContext(opts);
            var svc = new SensorService(ctx);

            // Act: attempt to delete nonexistent sensor
            var result = await svc.DeleteSensorAsync(9999);

            // Assert: returns false
            Assert.False(result);
        }
    }
}
