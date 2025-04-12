using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET09102_Coursework.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("user_id")]
    public int Id { get; set; }

    [Required]
    [Column("first_name")]
    public string FirstName { get; set; }

    [Required]
    [Column("surname")]
    public string Surname { get; set; }

    [Required]
    [Column("street")]
    public string Street { get; set; }

    [Required]
    [Column("city")]
    public string City { get; set; }
    
    [Required]
    [Column("postcode")]
    public string Postcode { get; set; }

    [Required]
    [Column("email")]
    public string Email { get; set; }

    [Required]
    [Column("password")]
    public string Password { get; set; } 

    [ForeignKey("Role")]
    [Column("role_id")]
    public int RoleId { get; set; }


    // Navigation Properties
    public Role Role { get; set; }
}
