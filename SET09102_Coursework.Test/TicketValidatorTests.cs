using System;
using SET09102_Coursework.Models;
using SET09102_Coursework.Validation;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="TicketValidator"/>.
    /// Verifies that the validator correctly identifies valid and invalid tickets.
    /// </summary>
    public class TicketValidatorTests
    {
        private readonly TicketValidator _validator = new();

        /// <summary>
        /// Validate should return failure if the ticket instance is null.
        /// </summary>
        [Fact]
        public void Validate_IssueDescriptionNullOrWhitespace_ReturnsInvalid()
        {
            // Arrange: create tickets with null or whitespace issue descriptions
            var ticket1 = new SensorTicket { IssueDescription = null! };
            var ticket2 = new SensorTicket { IssueDescription = "" };
            var ticket3 = new SensorTicket { IssueDescription = "    " };

            // Act: validate the tickets
            var result1 = _validator.Validate(ticket1);
            var result2 = _validator.Validate(ticket2);
            var result3 = _validator.Validate(ticket3);

            // Assert: all should be invalid and contain the appropriate error message
            Assert.False(result1.IsValid);
            Assert.Equal("Issue description cannot be empty.", result1.ErrorMessage);

            Assert.False(result2.IsValid);
            Assert.Equal("Issue description cannot be empty.", result2.ErrorMessage);

            Assert.False(result3.IsValid);
            Assert.Equal("Issue description cannot be empty.", result3.ErrorMessage);
        }

        /// <summary>
        /// Validate should return failure when issue description is too short.
        /// </summary>
        [Fact]
        public void Validate_IssueDescriptionTooShort_ReturnsInvalid()
        {
            // Arrange: create a ticket with a short issue description
            var ticket = new SensorTicket { IssueDescription = "TooShort" }; // 8 chars

            // Act: validate the ticket
            var result = _validator.Validate(ticket);

            // Assert: result should be invalid and contain the appropriate error message
            Assert.False(result.IsValid);
            Assert.Equal("Description must be at least 10 characters.", result.ErrorMessage);
        }

        /// <summary>
        /// Validate should return valid when issue description is long enough.
        /// /// </summary>
        [Fact]
        public void Validate_IssueDescriptionLongEnough_ReturnsValid()
        {
            // Arrange: create a ticket with a valid issue description
            var ticket = new SensorTicket { IssueDescription = "This is long enough." };

            // Act: validate the ticket
            var result = _validator.Validate(ticket);

            // Assert: result should be valid and contain no error message
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }
    }
}
