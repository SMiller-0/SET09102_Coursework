using System.Collections.Generic;
using System.Linq;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

public interface ITicketValidator
{
    (bool IsValid, string ErrorMessage) Validate(SensorTicket ticket);
}
