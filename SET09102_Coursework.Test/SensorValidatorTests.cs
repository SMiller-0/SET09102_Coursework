using System.Text.RegularExpressions;
using SET09102_Coursework.Models;
using SET09102_Coursework.Validation;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="SensorValidator"/>.
    /// Verifies that the validator correctly identifies valid and invalid sensor instances.
    /// </summary>
    public class SensorValidatorTests
    {
        private readonly SensorValidator _validator = new SensorValidator();

        /// <summary>
        /// Validate should return failure if the sensor instance is null.
        /// </summary>
        [Fact]
        public void Validate_NullSensor_ReturnsFailure()
        {
            // Arrange: null sensor instance
            Sensor sensor = null!;

            // Act: validate the null sensor
            var result = _validator.Validate(sensor);

            // Assert: result should be invalid and contain the appropriate error message
            Assert.False(result.IsValid);
            Assert.Equal("Sensor cannot be null.", result.ErrorMessage);
        }

        /// <summary>
        /// Validate should return failure when name is null or whitespace.
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_EmptyName_ReturnsFailure(string name)
        {
            // Arrange: create a sensor with an empty name
            var sensor = new Sensor { Name = name! };

            // Act: validate the sensor
            var result = _validator.Validate(sensor);

            // Assert: result should be invalid and contain the appropriate error message
            Assert.False(result.IsValid);
            Assert.Equal("Sensor name is required.", result.ErrorMessage);
        }

        /// <summary>
        /// Validate should return failure when name exceeds 100 characters.
        /// </summary>
        [Fact]
        public void Validate_NameTooLong_ReturnsFailure()
        {
            // Arrange: create a sensor with a name longer than 100 characters
            var sensor = new Sensor { Name = new string('X', 101) };

            // Act: validate the sensor
            var result = _validator.Validate(sensor);

            // Assert: result should be invalid and contain the appropriate error message
            Assert.False(result.IsValid);
            Assert.Equal("Sensor name must be 100 characters or fewer.", result.ErrorMessage);
        }

        /// <summary>
        /// Validate should return failure when firmware version is null or whitespace.
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_EmptyFirmwareVersion_ReturnsFailure(string version)
        {
            // Arrange: create a sensor with an empty firmware version
            var sensor = new Sensor { Name = "ValidName", FirmwareVersion = version! };

            // Act: validate the sensor
            var result = _validator.Validate(sensor);

            // Assert: result should be invalid and contain the appropriate error message
            Assert.False(result.IsValid);
            Assert.Equal("Firmware version is required.", result.ErrorMessage);
        }

        /// <summary>
        /// Validate should return failure when firmware version does not match X.Y.Z format.
        /// </summary>
        [Theory]
        [InlineData("1")]
        [InlineData("1.2")]
        [InlineData("1.2.3.4")]
        [InlineData("a.b.c")]
        public void Validate_InvalidFirmwareFormat_ReturnsFailure(string version)
        {
            // Arrange: create a sensor with an invalid firmware version format
            var sensor = new Sensor
            {
                Name = "ValidName",
                FirmwareVersion = version
            };

            // Act: validate the sensor
            var result = _validator.Validate(sensor);

            // Assert: result should be invalid and contain the appropriate error message
            Assert.False(result.IsValid);
            Assert.Equal("Firmware version must be in format X.Y.Z (e.g. 1.0.0)", result.ErrorMessage);
        }


        /// <summary>
    /// Validate should return failure when latitude is out of [-90,90].
    /// </summary>
    [Theory]
    [InlineData(-91.0)]
    [InlineData( 91.0)]
    public void Validate_InvalidLatitude_ReturnsFailure(double latitudeInput)
    {
        // Arrange: create a sensor with an invalid latitude
        var sensor = new Sensor
        {
            Name            = "ValidName",
            FirmwareVersion = "1.0.0",
            Latitude        = (decimal)latitudeInput
        };

        // Act: validate the sensor
        var result = _validator.Validate(sensor);

        // Assert: result should be invalid and contain the appropriate error message
        Assert.False(result.IsValid);
        Assert.Equal("Latitude must be between -90 and 90.", result.ErrorMessage);
    }

    /// <summary>
    /// Validate should return failure when longitude is out of [-180,180].
    /// </summary>
    [Theory]
    [InlineData(-181.0)]
    [InlineData( 181.0)]
    public void Validate_InvalidLongitude_ReturnsFailure(double longitudeInput)
    {
        // Arrange: create a sensor with an invalid longitude
        var sensor = new Sensor
        {
            Name            = "ValidName",
            FirmwareVersion = "1.0.0",
            Latitude        = 0M,
            Longitude       = (decimal)longitudeInput
        };

        // Act: validate the sensor
        var result = _validator.Validate(sensor);

        // Assert: result should be invalid and contain the appropriate error message
        Assert.False(result.IsValid);
        Assert.Equal("Longitude must be between -180 and 180.", result.ErrorMessage);
    }

        /// <summary>
        /// Validate should return success for a completely valid sensor.
        /// </summary>
        [Fact]
        public void Validate_ValidSensor_ReturnsSuccess()
        {
            // Arrange: create a sensor with valid properties
            var sensor = new Sensor
            {
                Name            = "Thermometer",
                FirmwareVersion = "2.3.4",
                Latitude        =  45.5M,
                Longitude       = 12.3M,
                SensorTypeId    = 1
            };

            // Act: validate the sensor
            var result = _validator.Validate(sensor);

            // Assert: result should be valid and contain no error message
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);

        }
    }
}

