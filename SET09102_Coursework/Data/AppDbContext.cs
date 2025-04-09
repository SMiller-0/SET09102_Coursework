using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Data;
public class AppDbContext : DbContext
{

    public AppDbContext()
    { }
    public AppDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

}
