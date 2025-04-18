using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

public interface ISettingsValidator
{
    ValidationResult Validate(Settings settings);
    ValidationResult ValidateCollection(IEnumerable<Settings> settings);
}
