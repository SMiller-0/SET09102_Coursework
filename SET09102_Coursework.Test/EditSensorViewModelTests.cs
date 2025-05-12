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
    /// Unit tests for <see cref="EditSensorViewModel"/>.
    /// Verifies SaveCommand handles various input scenarios correctly.
    /// </summary>
    public class EditSensorViewModelTests
    {
        private readonly Mock<ISensorService> _mockService;
        private readonly Mock<INavigationService> _mockNav;
        private readonly Mock<ISensorValidator> _mockValidator;
        private readonly EditSensorViewModel _vm;

        public EditSensorViewModelTests()
        {
            _mockService   = new Mock<ISensorService>();
            _mockNav       = new Mock<INavigationService>();
            _mockValidator = new Mock<ISensorValidator>();
            _vm = new EditSensorViewModel(
                _mockService.Object,
                _mockNav.Object,
                _mockValidator.Object);
            // make sure there's a Sensor object to work on
            _vm.Sensor = new Sensor { Id = 1, SensorTypeId = 1 };
        }

        [Fact]
        public async Task SaveCommand_MissingName_SetsErrorMessage()
        {
            // Arrange
            _vm.Name            = "";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput   = "45.0";
            _vm.LongitudeInput  = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };

            // Act
            await _vm.SaveCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Please fill in all required fields.", _vm.ErrorMessage);
        }

        [Fact]
        public async Task SaveCommand_InvalidLatitude_SetsErrorMessage()
        {
            // Arrange
            _vm.Name            = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput   = "not a number";
            _vm.LongitudeInput  = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };

            // Act
            await _vm.SaveCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Latitude must be a valid number.", _vm.ErrorMessage);
        }

        [Fact]
        public async Task SaveCommand_InvalidLongitude_SetsErrorMessage()
        {
            // Arrange
            _vm.Name            = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput   = "45.0";
            _vm.LongitudeInput  = "not a number";
            _vm.SelectedSensorType = new SensorType { Id = 1 };

            // Act
            await _vm.SaveCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Longitude must be a valid number.", _vm.ErrorMessage);
        }

        [Fact]
        public async Task SaveCommand_ValidatorFails_SetsErrorMessage()
        {
            // Arrange
            _vm.Name            = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput   = "45.0";
            _vm.LongitudeInput  = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };
            _mockValidator
                .Setup(v => v.Validate(It.IsAny<Sensor>()))
                .Returns(ValidationResult.Failure("Bad sensor"));

            // Act
            await _vm.SaveCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Bad sensor", _vm.ErrorMessage);
            _mockService.Verify(s => s.UpdateSensorAsync(It.IsAny<Sensor>()), Times.Never);
        }

        [Fact]
        public async Task SaveCommand_ServiceFails_SetsErrorMessage()
        {
            // Arrange
            _vm.Name            = "Sensor";
            _vm.FirmwareVersion = "1.0.0";
            _vm.LatitudeInput   = "45.0";
            _vm.LongitudeInput  = "90.0";
            _vm.SelectedSensorType = new SensorType { Id = 1 };
            _mockValidator
                .Setup(v => v.Validate(It.IsAny<Sensor>()))
                .Returns(ValidationResult.Success());
            _mockService
                .Setup(s => s.UpdateSensorAsync(It.IsAny<Sensor>()))
                .ReturnsAsync(false);

            // Act
            await _vm.SaveCommand.ExecuteAsync(null);

            // Assert
            Assert.True(_vm.HasError);
            Assert.Equal("Failed to update sensor.", _vm.ErrorMessage);
        }

        
    }
}
