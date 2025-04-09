using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("sensor")]
[PrimaryKey(nameof(Id))]
public class Sensor
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Longitude { get; set; }
    [Required]
    public string Latitude { get; set; }
    [Required]
    public bool IsActive { get; set; } = true;

    [ForeignKey("SensorType")]
    public int SensorTypeId { get; set; }
    public SensorType SensorType { get; set; }
}
