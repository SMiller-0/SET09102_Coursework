using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Models;
using System.Data;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace SET09102_Coursework.Data;
public class AppDbContext : DbContext
{

    public AppDbContext()
    { }
    public AppDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Sensor> Sensors { get; set; }

    public DbSet<SensorType> SensorTypes { get; set; }

    public DbSet<AirQData> AirQData { get; set; }

    public DbSet<WaterQData> WaterQData { get; set; }

    public DbSet<WeatherData> WeatherData { get; set; }
}
