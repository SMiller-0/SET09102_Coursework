using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("role")]
public class Role
{
    [Key]
    [Column("role_id")]
    public int RoleId { get; set; }

    [Required]
    [Column("role_name")]
    public string RoleName { get; set; }
}
