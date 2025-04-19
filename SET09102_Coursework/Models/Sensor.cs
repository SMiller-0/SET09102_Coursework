using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("sensor")]
[PrimaryKey(nameof(Id))]
public class Sensor
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Required]
    [Column("longitude")]
    public decimal Longitude { get; set; }

    [Required]
    [Column("latitude")]
    public decimal Latitude { get; set; }

    [Required]
    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Required]
    [Column("firmware_version")]
    public string FirmwareVersion { get; set; }

    [Required]
    [ForeignKey("SensorType")]
    [Column("sensor_type_id")]
    public int SensorTypeId { get; set; }

    // Navigation property
    public SensorType SensorType { get; set; }
}
