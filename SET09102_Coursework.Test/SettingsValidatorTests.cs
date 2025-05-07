using SET09102_Coursework.Models;
using SET09102_Coursework.Validation;
using SET09102_Coursework.Services;
using System.Collections.Generic;
using Xunit;

namespace SET09102_Coursework.Test
{
    public class SettingsValidatorTests
    {
        /// <summary>
        /// Tests that validator accepts valid settings
        /// </summary>
        [Fact]
        public void ValidateCollection_ValidSettings_ReturnsValid()
        {
            // Arrange
            var settings = new List<Settings>
            {
                new Settings { MinimumValue = 10, MaximumValue = 20 },
                new Settings { MinimumValue = 0, MaximumValue = 100 }
            };
            
            var validator = new SettingsValidator();
            
            // Act
            var result = validator.ValidateCollection(settings);
            
            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.ErrorMessage);
        }

        /// <summary>
        /// Tests that validator rejects when min > max
        /// </summary>
        [Fact]
        public void ValidateCollection_MinGreaterThanMax_ReturnsInvalid()
        {
            // Arrange
            var settings = new List<Settings>
            {
                new Settings { MinimumValue = 30, MaximumValue = 20 }
            };
            
            var validator = new SettingsValidator();
            
            // Act
            var result = validator.ValidateCollection(settings);
            
            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("minimum", result.ErrorMessage.ToLower());
            Assert.Contains("maximum", result.ErrorMessage.ToLower());
        }

        /// <summary>
        /// Tests that validator rejects null collection
        /// </summary>
        [Fact]
        public void ValidateCollection_NullCollection_ReturnsInvalid()
        {
            // Arrange
            IEnumerable<Settings> settings = null;
            var validator = new SettingsValidator();
            
            // Act
            var result = validator.ValidateCollection(settings);
            
            // Assert
            Assert.False(result.IsValid);
        }
    }
}
