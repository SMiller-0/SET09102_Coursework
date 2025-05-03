using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models
{
    [Table("ticket_response")]
    public class TicketResponse
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ticket_id")]
        public int TicketId { get; set; }

        [Required]
        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        [ForeignKey(nameof(TicketId))]
        public SensorTicket Ticket { get; set; } = null!;

    }
}
