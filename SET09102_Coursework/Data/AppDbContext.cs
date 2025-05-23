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
    public DbSet<Role> Roles { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<SensorType> SensorTypes { get; set; }
    public DbSet<SettingType> SettingTypes { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<SensorTicket> SensorTickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<TicketResponse> TicketResponses { get; set; }
    public DbSet<AirMeasurement> AirMeasurements { get; set; }
    public DbSet<WaterMeasurement> WaterMeasurements { get; set; }
    public DbSet<WeatherMeasurement> WeatherMeasurements { get; set; }
}
