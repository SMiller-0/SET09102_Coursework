using System.Globalization;

namespace SET09102_Coursework.Converters;

/// <summary>
/// Converts boolean values to "Active" or "Inactive" string representations.
/// </summary>
/// <remarks>
/// This converter is used in XAML bindings to display boolean status values in a user-friendly format.
/// Implements IValueConverter for use in XAML data binding.
/// </remarks>
public class BoolToActiveConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean value to its string representation.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// "Active" if the value is true,
    /// "Inactive" if the value is false,
    /// "Unknown" if the value is not a boolean.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            return isActive ? "Active" : "Inactive";
        }
        return "Unknown";
    }

    /// <summary>
    /// Converts a string back to a boolean value (not implemented).
    /// </summary>
    /// <param name="value">The value to convert back.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>Nothing, throws NotImplementedException.</returns>
    /// <exception cref="NotImplementedException">This method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
