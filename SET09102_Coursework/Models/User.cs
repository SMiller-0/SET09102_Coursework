using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Models;

[Table("user")]
[PrimaryKey(nameof(Id))]
public class User
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role Role { get; set; }

}
