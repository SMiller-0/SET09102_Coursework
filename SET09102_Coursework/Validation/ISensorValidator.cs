using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

/// <summary>
/// Defines validation logic for checking the integrity and formatting of sensor data before saving.
/// </summary>
public interface ISensorValidator
{
    /// <summary>
    /// Validates a sensor object and returns a <see cref="ValidationResult"/>.
    /// </summary>
    /// <param name="sensor">The sensor to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating success or failure, with a message if validation fails.
    /// </returns>
    /// <remarks>
    /// This method checks the following conditions:
    /// - The sensor object is not null.
    /// - The sensor name is not empty and does not exceed 100 characters.
    /// - The firmware version is not empty and follows the format X.Y.Z (e.g. 1.0.0).
    /// - The latitude is within the range of -90 to 90.
    /// - The longitude is within the range of -180 to 180.
    /// - The sensor type ID is valid (greater than 0).
    /// </remarks>
    ValidationResult Validate(Sensor sensor);
}
