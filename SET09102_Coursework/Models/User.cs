using System;

namespace SET09102_Coursework.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }

}
