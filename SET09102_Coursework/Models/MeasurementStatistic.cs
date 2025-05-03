using System;
using System.Collections.Generic;

namespace SET09102_Coursework.Models;

public class MeasurementStatistic
{
    public string ParameterName { get; set; }
    public double MaximumValue { get; set; }
    public double MinimumValue { get; set; }
    public double AverageValue { get; set; }
    public double ModeValue { get; set; }
    public double LatestValue { get; set; }
}
