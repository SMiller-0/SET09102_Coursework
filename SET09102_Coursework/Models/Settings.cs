using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("settings")]
[PrimaryKey(nameof(Id))]
public class Settings
{
    [Column("id")]
    public int Id { get; set; }
    
    [Required]    
    [ForeignKey("SettingType")]
    [Column("setting_type_id")]    
    public int SettingTypeId { get; set; }

    [Required]
    [ForeignKey("Sensor")]    
    [Column("sensor_id")]
    public int SensorId { get; set; }

    [Required]    
    [Column("minimum_value")]
    public decimal MinimumValue { get; set; }

    [Required]    
    [Column("maximum_value")]
    public decimal MaximumValue { get; set; }

    [Required]    
    [Column("current_value")]
    public decimal CurrentValue { get; set; }

    // Navigation properties    
    public SettingType SettingType { get; set; }
    public Sensor Sensor { get; set; }
}

