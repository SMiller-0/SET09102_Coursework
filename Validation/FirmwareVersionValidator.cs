using System.Text.RegularExpressions;

namespace SET09102_Coursework.Validators;

/// <summary>
/// Provides validation functionality for firmware version strings.
/// </summary>
/// <remarks>
/// This static class ensures that firmware versions follow the semantic versioning format X.Y.Z,
/// where X, Y, and Z are non-negative integers.
/// </remarks>
public static class FirmwareVersionValidator
{
    /// <summary>
    /// Regular expression pattern that matches valid firmware version formats.
    /// </summary>
    /// <remarks>
    /// The pattern matches strings in the format "X.Y.Z" where X, Y, and Z are sequences of digits.
    /// For example: "1.0.0", "2.15.3", etc.
    /// </remarks>
    private static readonly Regex VersionPattern = new(@"^\d+\.\d+\.\d+$");

    /// <summary>
    /// Validates whether a given string represents a valid firmware version.
    /// </summary>
    /// <param name="version">The firmware version string to validate.</param>
    /// <returns>
    /// True if the version string is not null or whitespace and matches the X.Y.Z format;
    /// otherwise, false.
    /// </returns>
    /// <remarks>
    /// Valid version examples:
    /// - "1.0.0"
    /// - "2.15.3"
    /// - "10.2.345"
    /// 
    /// Invalid version examples:
    /// - null
    /// - "" (empty string)
    /// - "1.0"
    /// - "1.0.0.0"
    /// - "1.a.0"
    /// - "v1.0.0"
    /// </remarks>
    public static bool IsValid(string version)
    {
        return !string.IsNullOrWhiteSpace(version) && VersionPattern.IsMatch(version);
    }
}
