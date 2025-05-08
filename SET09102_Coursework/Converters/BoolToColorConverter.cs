using System.Globalization;

namespace SET09102_Coursework.Converters;

/// <summary>
/// Converts boolean values to color values for visual status indication.
/// </summary>
/// <remarks>
/// This converter is used in XAML bindings to provide visual feedback through colors.
/// It converts true/false values to appropriate colors:
/// - True: LightGreen (indicating active/success state)
/// - False: LightPink (indicating inactive/warning state)
/// - Invalid input: Gray (indicating error/unknown state)
/// Implements IValueConverter for use in XAML data binding.
/// </remarks>
public class BoolToColorConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean value to its corresponding color representation.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// Colors.LightGreen if the value is true,
    /// Colors.LightPink if the value is false,
    /// Colors.Gray if the value is not a boolean.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            return isActive ? Colors.LightGreen : Colors.LightPink;
        }
        return Colors.Gray;
    }

    /// <summary>
    /// Converts a color back to a boolean value (not implemented).
    /// </summary>
    /// <param name="value">The color value to convert back.</param>
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
