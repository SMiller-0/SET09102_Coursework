using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("weatherData")]
[PrimaryKey(nameof(Id))]
public class WeatherData
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("airTemperature")]
    public string AirTemp { get; set; }

    [Required]
    [Column("humidity")]
    public string Humidity { get; set; }

    [Required]
    [Column("windSpeed")]
    public string WindSpeed { get; set; }

    [Required]
    [Column("windDirection")]
    public string WindDirection { get; set; }

    [ForeignKey("Reading")]
    [Column("reading_id")]
    public int ReadingId { get; set; }
}
