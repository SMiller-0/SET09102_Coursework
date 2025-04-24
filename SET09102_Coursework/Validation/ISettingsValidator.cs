using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

/// <summary>
/// Defines the contract for validating Settings objects and collections.
/// </summary>
/// <remarks>
/// This interface provides methods for validating both individual Settings objects
/// and collections of Settings objects to ensure they meet business rules and data constraints.
/// Implementations should verify properties such as minimum/maximum values and other business-specific rules.
/// </remarks>
public interface ISettingsValidator
{
    /// <summary>
    /// Validates a single Settings object.
    /// </summary>
    /// <param name="settings">The Settings object to validate.</param>
    /// <returns>
    /// A ValidationResult object indicating whether the settings are valid.
    /// The result contains:
    /// - IsValid: Boolean indicating if validation passed
    /// - ErrorMessage: Description of the validation failure if invalid
    /// </returns>
    /// <remarks>
    /// Implementations should validate:
    /// - Settings object is not null
    /// - Minimum value is less than maximum value
    /// - Any other business-specific rules for settings
    /// </remarks>
    ValidationResult Validate(Settings settings);

    /// <summary>
    /// Validates a collection of Settings objects.
    /// </summary>
    /// <param name="settings">The collection of Settings objects to validate.</param>
    /// <returns>
    /// A ValidationResult object indicating whether all settings in the collection are valid.
    /// The result contains:
    /// - IsValid: Boolean indicating if all settings passed validation
    /// - ErrorMessage: Description of the first validation failure encountered
    /// </returns>
    /// <remarks>
    /// Implementations should:
    /// - Check if the collection is not null
    /// - Validate each individual Settings object
    /// - Return the first validation failure encountered
    /// - Return success only if all settings are valid
    /// </remarks>
    ValidationResult ValidateCollection(IEnumerable<Settings> settings);
}
