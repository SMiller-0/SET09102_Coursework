using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("air_measurements")]
[PrimaryKey(nameof(ID))]
public class AirMeasurement
{
    [Column("ID")]
    public int ID { get; set; }

    [Required]
    [Column("date_time")]
    public DateTime DateTime { get; set; }

    [Column("nitrogen_dioxide")]
    public double NitrogenDioxide { get; set; }

    [Column("sulphur_dioxide")]
    public double SulphurDioxide { get; set; }

    [Column("pm2_5")]
    public double PM25 { get; set; }

    [Column("pm10")]
    public double PM10 { get; set; }

    [Column("sensor_id")]
    public int SensorId { get; set; }

    // Navigation property
    public Sensor Sensor { get; set; }
}