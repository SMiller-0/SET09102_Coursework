using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using SET09102_Coursework.Converters;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for the StringNotNullOrEmptyConverter to ensure it correctly evaluates
    /// whether a given string is not null, empty, or whitespace.
    /// </summary>
    public class StringNotNullOrEmptyConverterTests
    {
        private readonly StringNotNullOrEmptyConverter _conv = new();

        /// <summary>
        /// Tests the Convert method for various string inputs, verifying
        /// it returns false for null, empty, or whitespace-only strings,
        /// and true for non-empty strings.
        /// </summary>
        /// <param name="input">The value to convert (should be string or null).</param>
        /// <param name="expected">Expected boolean result.</param>
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("hello", true)]
        public void Convert_VariousStrings_ReturnsExpected(object input, bool expected)
        {
            // Arrange: converter is stateless, no setup required

            // Act: call Convert with the test input
            var result = _conv.Convert(input, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert: result is a bool and matches expected value
            Assert.IsType<bool>(result);
            Assert.Equal(expected, (bool)result);
        }

        /// <summary>
        /// Tests that ConvertBack always throws NotImplementedException,
        /// since only forward conversion is supported.
        /// </summary>
        [Fact]
        public void ConvertBack_Always_ThrowsNotImplemented()
        {
            // Arrange: no setup needed

            // Act & Assert: calling ConvertBack should throw
            Assert.Throws<NotImplementedException>(() =>
                _conv.ConvertBack(true, typeof(string), null, CultureInfo.InvariantCulture));
        }
    }
}
