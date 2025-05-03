using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("weather_measurements")]
[PrimaryKey(nameof(ID))]
public class WeatherMeasurement
{
    [Column("ID")]
    public int ID { get; set; }

    [Required]
    [Column("date_time")]
    public string DateTime { get; set; }

    [Column("temperature_2m")]
    public double Temperature2m { get; set; }

    [Column("relative_humidity_2m")]
    public double RelativeHumidity2m { get; set; }

    [Column("wind_speed_10m")]
    public double WindSpeed10m { get; set; }

    [Column("wind_direction_10m")]
    public double WindDirection10m { get; set; }

    [Column("sensor_id")]
    public int SensorId { get; set; }

    // Navigation property
    public Sensor Sensor { get; set; }
}