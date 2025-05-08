using System.Collections.Generic;
using System.Linq;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

/// inheritdoc/>
public class TicketValidator : ITicketValidator
{

    /// inheritdoc/>
    public (bool IsValid, string ErrorMessage) Validate(SensorTicket ticket)
    {
        if (string.IsNullOrWhiteSpace(ticket.IssueDescription))
            return (false, "Issue description cannot be empty.");

        if (ticket.IssueDescription.Length < 10)
            return (false, "Description must be at least 10 characters.");

        return (true, string.Empty);
    }
}
