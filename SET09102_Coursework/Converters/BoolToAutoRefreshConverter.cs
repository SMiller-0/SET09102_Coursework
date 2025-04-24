using System.Globalization;

namespace SET09102_Coursework.Converters;

public class BoolToAutoRefreshConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "Stop Auto-Refresh" : "Start Auto-Refresh";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}