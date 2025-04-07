using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("users")]
[PrimaryKey(nameof(Id))]
public class User
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("first_name")]
    public string FirstName { get; set; }

    [Required]
    [Column("surname")]
    public string Surname { get; set; }

    [Required]
    [Column("email")]
    public string Email { get; set; }

    [ForeignKey("Role")]
    [Column("role_id")]
    public int RoleId { get; set; }

    public Role Role { get; set; }
}
