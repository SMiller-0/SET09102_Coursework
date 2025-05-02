using System.Collections.Generic;
using System.Linq;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

/// <summary>
/// Validator for sensor tickets. Ensures that the ticket data is valid before submission.
/// </summary>
public interface ITicketValidator
{
    /// <summary>
    /// Validates the sensor ticket data.
    /// </summary>
    /// <param name="ticket">The sensor ticket to validate.</param>
    /// <returns>A tuple indicating whether the ticket is valid and an error message if invalid.</returns>
    (bool IsValid, string ErrorMessage) Validate(SensorTicket ticket);
}
