using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;

namespace SET09102_Coursework.Services;

public class TicketService : ITicketService
{
private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateTicketAsync(SensorTicket ticket)
    {
        try
        {
            _context.SensorTickets.Add(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to create ticket: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<SensorTicket>> GetTicketsBySensorAsync(int sensorId)
    {
        try
        {
            return await _context.SensorTickets
                .Where(t => t.SensorId == sensorId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to load tickets for sensor {sensorId}: {ex.Message}");
            return Enumerable.Empty<SensorTicket>();
        }
    }

    public async Task<IEnumerable<SensorTicket>> GetAllTicketsAsync()
    {
        try
        {
            return await _context.SensorTickets
                .Include(t => t.Status)
                .Include(t => t.Sensor)
                .OrderBy(t =>
                    t.Status.StatusName == "Open" ? 1 :
                    t.Status.StatusName == "Under Investigation" ? 2 : 3)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to load tickets: {ex.Message}");
            return Enumerable.Empty<SensorTicket>();
        }
    }

   /*  public async Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatus)
    {
        try
        {
            var ticket = await _context.SensorTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            var statusObj = await _context.TicketStatuses.FirstOrDefaultAsync(s => s.StatusName == newStatus);
            if (statusObj == null) return false;

            ticket.Status = statusObj;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to update ticket status: {ex.Message}");
            return false;
        }
    } */


   public async Task<TicketStatus?> GetStatusByTypeAsync(string name)
   {   
        return await _context.TicketStatuses.FirstOrDefaultAsync(s => s.StatusName == name);
  }

    //public async Task<IEnumerable<TicketType>> GetTicketTypesAsync()
    //{        return await _context.TicketTypes.OrderBy(st => st.Name).ToListAsync();
    //}

}