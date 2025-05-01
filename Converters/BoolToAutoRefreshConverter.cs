using System.Globalization;

namespace SET09102_Coursework.Converters;

/// <summary>
/// Converts boolean values to auto-refresh status text for UI display.
/// </summary>
/// <remarks>
/// This converter is used in XAML bindings to display the current auto-refresh state
/// as user-friendly text. It converts true/false values to appropriate action text
/// indicating whether auto-refresh can be started or stopped.
/// Implements IValueConverter for use in XAML data binding.
/// </remarks>
public class BoolToAutoRefreshConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean value to its corresponding auto-refresh status text.
    /// </summary>
    /// <param name="value">The boolean value representing the current auto-refresh state.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// "Stop Auto-Refresh" if the value is true (auto-refresh is active),
    /// "Start Auto-Refresh" if the value is false (auto-refresh is inactive).
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "Stop Auto-Refresh" : "Start Auto-Refresh";
    }

    /// <summary>
    /// Converts a string back to a boolean value (not implemented).
    /// </summary>
    /// <param name="value">The string value to convert back.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>Nothing, throws NotImplementedException.</returns>
    /// <exception cref="NotImplementedException">This method is not implemented as conversion is only needed in one direction.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
