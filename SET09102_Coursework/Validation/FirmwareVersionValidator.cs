using System.Text.RegularExpressions;

namespace SET09102_Coursework.Validators;

public static class FirmwareVersionValidator
{
    private static readonly Regex VersionPattern = new(@"^\d+\.\d+\.\d+$");

    public static bool IsValid(string version)
    {
        return !string.IsNullOrWhiteSpace(version) && VersionPattern.IsMatch(version);
    }
}