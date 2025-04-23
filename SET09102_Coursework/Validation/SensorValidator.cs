using System.Text.RegularExpressions;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Validation;

public class SensorValidator: ISensorValidator
{
    public ValidationResult Validate(Sensor sensor)
    {
        if (sensor == null)
        {
            return ValidationResult.Failure("Sensor cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(sensor.Name))
        {
            return ValidationResult.Failure("Sensor name is required.");
        }

        if (sensor.Name.Length > 100)
        {
            return ValidationResult.Failure("Sensor name must be 100 characters or fewer.");
        }

        if (string.IsNullOrWhiteSpace(sensor.FirmwareVersion))
        {
            return ValidationResult.Failure("Firmware version is required.");
        }

        var versionPattern = @"^\d+\.\d+\.\d+$";
        if (!Regex.IsMatch(sensor.FirmwareVersion, versionPattern))
        {
            return ValidationResult.Failure("Firmware version must be in format X.Y.Z (e.g. 1.0.0)");
        }

        if (sensor.Latitude == null || sensor.Latitude < -90 || sensor.Latitude > 90)
        {
            return ValidationResult.Failure("Latitude must be between -90 and 90.");
        }

        if (sensor.Longitude == null || sensor.Longitude < -180 || sensor.Longitude > 180)
        {
            return ValidationResult.Failure("Longitude must be between -180 and 180.");
        }

        if (sensor.SensorTypeId <= 0)
        {
            return ValidationResult.Failure("A valid sensor type must be selected.");
        }

        return ValidationResult.Success();
    }

}
