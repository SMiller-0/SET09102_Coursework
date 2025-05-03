using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("water_measurements")]
[PrimaryKey(nameof(ID))]
public class WaterMeasurement
{
    [Column("ID")]
    public int ID { get; set; }

    [Required]
    [Column("date_time")]
    public string DateTime { get; set; }

    [Column("nitrate")]
    public double Nitrate { get; set; }

    [Column("nitrite")]
    public double Nitrite { get; set; }

    [Column("phosphate")]
    public double Phosphate { get; set; }

    [Column("sensor_id")]
    public int SensorId { get; set; }

    // Navigation property
    public Sensor Sensor { get; set; }
}