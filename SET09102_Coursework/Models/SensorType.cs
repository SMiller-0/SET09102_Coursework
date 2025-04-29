using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("sensor_type")]
[PrimaryKey(nameof(Id))]
public class SensorType
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

}
