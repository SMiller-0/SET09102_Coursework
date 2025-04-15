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
    public string Date { get; set; }

    [Required]
    [Column("nitriteLevel")]
    public string Nitrite { get; set; }

    [Required]
    [Column("nitrateLevel")]
    public string Nitrate { get; set; }

    [Required]
    [Column("phosphate")]
    public string Phosphate { get; set; }

    [Required]
    [Column("escherichiaColi")]
    public string EscherichiaColi { get; set; }

    [Required]
    [Column("intestinalEnterococci")]
    public string IntestinalEnterococci { get; set; }

    [ForeignKey("Reading")]
    [Column("reading_id")]
    public int ReadingId { get; set; }
}
