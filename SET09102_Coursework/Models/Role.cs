using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("role")]
[PrimaryKey(nameof(Id))]
public class Role
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

}

