using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("sensor_type")]
[PrimaryKey(nameof(Id))]
public class SensorType
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }
}
