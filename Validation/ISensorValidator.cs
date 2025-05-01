using SET09102_Coursework.Models;


namespace SET09102_Coursework.Validation;

public interface ISensorValidator
{
    ValidationResult Validate(Sensor sensor);
}
