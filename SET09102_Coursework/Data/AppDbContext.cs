using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Data;
public class SensorsDbContext : DbContext
{

    public SensorsDbContext()
    { }
    public SensorsDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet Users { get; set; }

}
