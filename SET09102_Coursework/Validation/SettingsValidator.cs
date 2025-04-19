using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

public class SettingsValidator : ISettingsValidator
{
    public ValidationResult Validate(Settings settings)
    {
        if (settings == null)
        {
            return ValidationResult.Failure("Settings cannot be null");
        }

        if (settings.MinimumValue >= settings.MaximumValue)
        {
            return ValidationResult.Failure(
                $"Minimum value must be less than maximum value for {settings.SettingType?.Name ?? "setting"}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult ValidateCollection(IEnumerable<Settings> settings)
    {
        if (settings == null)
        {
            return ValidationResult.Failure("Settings collection cannot be null");
        }

        foreach (var setting in settings)
        {
            var result = Validate(setting);
            if (!result.IsValid)
            {
                return result;
            }
        }

        return ValidationResult.Success();
    }
}

