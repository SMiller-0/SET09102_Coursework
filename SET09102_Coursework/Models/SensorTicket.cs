using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Column("status_id")]
    public int StatusId { get; set; } 

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// Navigational properties
    public Sensor Sensor { get; set; } 

    [ForeignKey(nameof(StatusId))]
    public TicketStatus Status { get; set; } 
}
