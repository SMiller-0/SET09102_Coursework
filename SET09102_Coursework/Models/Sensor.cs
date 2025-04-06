using System;

namespace SET09102_Coursework.Models;

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public SensorType Type { get; set; }
    public bool IsActive { get; set; } = true;
}
