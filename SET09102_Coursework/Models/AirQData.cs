using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("airQData")]
[PrimaryKey(nameof(Id))]
public class AirQData
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("timestamp")]
    public DateTime Date { get; set; }

    [Required]
    [Column("nitrogenLevel")]
    public int Nitrogen { get; set; }

    [Required]
    [Column("sulphurLevel")]
    public int Sulphur { get; set; }

    [Required]
    [Column("particleMatterSmall")]
    public int particleMatterSmall { get; set; }

    [Required]
    [Column("particleMatterBig")]
    public int particleMatterBig { get; set; }

    [ForeignKey("Reading")]
    [Column("reading_id")]
    public int ReadingId { get; set; }
}
