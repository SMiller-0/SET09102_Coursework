using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("waterQData")]
[PrimaryKey(nameof(Id))]
public class WaterQData
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("timestamp")]
    public DateTime Date { get; set; }

    [Required]
    [Column("nitriteLevel")]
    public int Nitrite { get; set; }

    [Required]
    [Column("nitrateLevel")]
    public int Nitrate { get; set; }

    [Required]
    [Column("phosphate")]
    public int Phosphate { get; set; }

    [Required]
    [Column("escherichiaColi")]
    public int EscherichiaColi { get; set; }

    [Required]
    [Column("intestinalEnterococci")]
    public int IntestinalEnterococci { get; set; }

    [ForeignKey("Reading")]
    [Column("reading_id")]
    public int ReadingId { get; set; }
}
