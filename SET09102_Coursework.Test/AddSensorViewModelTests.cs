using System.Threading.Tasks;
using Moq;
using Xunit;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validation;
using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="AddSensorViewModel"/>.
    /// Verifies SaveSensorCommand handles various input scenarios correctly.
    /// </summary>
    public class AddSensorViewModelTests
    {
        private readonly Mock<ISensorService> _mockService;
        private readonly Mock<INavigationService> _mockNav;
        private readonly Mock<ISensorValidator> _mockValidator;
        private readonly AddSensorViewModel _vm;

        public AddSensorViewModelTests()
        {
            _mockService = new Mock<ISensorService>();
            _mockNav     = new Mock<INavigationService>();
            _mockValidator = new Mock<ISensorValidator>();
            _vm = new AddSensorViewModel(_mockService.Object, _mockNav.Object, _mockValidator.Object);
        }

        /// <summary>
        /// Missing name should set HasError = true and show the "fill in" message.
        /// </summary>
        [Fact]
        public async Task SaveSensorCommand_MissingName_SetsErrorMessage()
        {
            // Arrange
            _vm.Name = string.Empty;
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput = "45.0";
            _vm.LongitudeInput = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };

            // Act
            await _vm.SaveSensorCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Please fill in all required fields.", _vm.ErrorMessage);
        }

        /// <summary>
        /// Non-numeric latitude should set HasError = true and show number error.
        /// </summary>
        [Fact]
        public async Task SaveSensorCommand_InvalidLatitude_SetsErrorMessage()
        {
            // Arrange
            _vm.Name = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput = "not a number";
            _vm.LongitudeInput = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };

            // Act
            await _vm.SaveSensorCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Latitude must be a valid number.", _vm.ErrorMessage);
        }

        /// <summary>
        /// Non-numeric longitude should set HasError = true and show number error.
        /// </summary>
        [Fact]
        public async Task SaveSensorCommand_InvalidLongitude_SetsErrorMessage()
        {
            // Arrange
            _vm.Name = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput = "45.0";
            _vm.LongitudeInput = "not a number";
            _vm.SelectedSensorType = new SensorType { Id = 1 };

            // Act
            await _vm.SaveSensorCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Longitude must be a valid number.", _vm.ErrorMessage);
        }

        /// <summary>
        /// Validator failure should propagate the validator's message.
        /// </summary>
        [Fact]
        public async Task SaveSensorCommand_ValidatorFails_SetsErrorMessage()
        {
            // Arrange
            _vm.Name = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput = "45.0";
            _vm.LongitudeInput = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };
            _mockValidator
                .Setup(v => v.Validate(It.IsAny<Sensor>()))
                .Returns(ValidationResult.Failure("Bad sensor"));

            // Act
            await _vm.SaveSensorCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Bad sensor", _vm.ErrorMessage);
        }

        /// <summary>
        /// Service save failure should set HasError = true and show service error.
        /// </summary>
        [Fact]
        public async Task SaveSensorCommand_ServiceFails_SetsErrorMessage()
        {
            // Arrange
            _vm.Name = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput = "45.0";
            _vm.LongitudeInput = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };
            _mockValidator
                .Setup(v => v.Validate(It.IsAny<Sensor>()))
                .Returns(ValidationResult.Success());
            _mockService
                .Setup(s => s.AddSensorAsync(It.IsAny<Sensor>()))
                .ReturnsAsync(false);

            // Act
            await _vm.SaveSensorCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("An error occurred while saving the sensor.", _vm.ErrorMessage);
        }
    }
}
