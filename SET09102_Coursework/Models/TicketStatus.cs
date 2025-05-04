using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("ticket_status")]
public class TicketStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("status_name")]
    public string StatusName { get; set; }

    // Navigational propery
    public ICollection<SensorTicket> Tickets { get; set; }
}
