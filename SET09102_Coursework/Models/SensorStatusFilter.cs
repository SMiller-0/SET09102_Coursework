namespace SET09102_Coursework.Models;

public class SensorStatusFilter
{
    public string DisplayName { get; set; } = string.Empty;
    public Func<Sensor, bool> FilterPredicate { get; set; } = _ => true;
}
