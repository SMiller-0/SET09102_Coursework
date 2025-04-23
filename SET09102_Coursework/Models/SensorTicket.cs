using System;

namespace SET09102_Coursework.Models;

[Table("sensor_ticket")]
public class SensorTicket
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("sensor_id")]
    public int SensorId { get; set; }

    [Required]
    [Column("issue_description")]
    public string IssueDescription { get; set; }

    [Required]
    [Column("status")]
    public string Status { get; set; } = "Open"; 

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Sensor Sensor { get; set; }

}
