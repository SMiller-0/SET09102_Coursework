using Microsoft.EntityFrameworkCore;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SET09102_Coursework.Models;
[Table("setting_type")][PrimaryKey(nameof(Id))]
public class SettingType{

     [Column("id")]
    public int Id { get; set; }
    
    [Required]    
    [Column("code")]
    public string Code { get; set; }

    [Required]    
    [Column("name")]
    public string Name { get; set; }

    [Required]    
    [Column("unit")]
    public string Unit { get; set; }

    [Required]    
    [ForeignKey("SensorType")]
    [Column("sensor_type_id")]    
    public int SensorTypeId { get; set; }


    // Navigation property
    public SensorType SensorType { get; set; }
}
















