using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

/// <summary>
/// Validates settings objects and collections to ensure they meet business rules.
/// </summary>
/// <remarks>
/// This validator ensures that settings have valid minimum and maximum values,
/// and that collections of settings are properly formatted.
/// Implements the ISettingsValidator interface.
/// </remarks>
public class SettingsValidator : ISettingsValidator
{
    /// <summary>
    /// Validates a single Settings object.
    /// </summary>
    /// <param name="settings">The Settings object to validate.</param>
    /// <returns>
    /// A ValidationResult indicating whether the settings are valid.
    /// If invalid, contains an error message describing the validation failure.
    /// </returns>
    /// <remarks>
    /// Performs the following validations:
    /// - Checks if the settings object is not null
    /// - Ensures minimum value is less than maximum value
    /// </remarks>
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

    /// <summary>
    /// Validates a collection of Settings objects.
    /// </summary>
    /// <param name="settings">The collection of Settings objects to validate.</param>
    /// <returns>
    /// A ValidationResult indicating whether all settings in the collection are valid.
    /// If any setting is invalid, contains an error message for the first validation failure encountered.
    /// </returns>
    /// <remarks>
    /// Performs the following validations:
    /// - Checks if the collection is not null
    /// - Validates each individual Settings object in the collection
    /// - Stops at the first validation failure encountered
    /// </remarks>
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

