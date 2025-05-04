using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;

namespace SET09102_Coursework.Services;

/// <inheritdoc/>
public class TicketService : ITicketService
{
  
    private readonly AppDbContext _context;

    /// <summary>
    /// Initialises a new instance of <see cref="TicketService"/>.
    /// </summary>
    /// <param name="context">EF Core database context.</param>  
    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<bool> ChangeTicketStatusAsync(int ticketId, int newStatusId)
    {
        try
        {
            var ticket = await _context.SensorTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.StatusId = newStatusId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SensorTicket>> GetTicketsByStatusAsync(int statusId)
    {
        return await _context.SensorTickets
            .Include(t => t.Status)
            .Include(t => t.Sensor)
            .Where(t => t.StatusId == statusId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TicketStatus>> GetAllTicketStatusesAsync()
    {
        return await _context.TicketStatuses
            .OrderBy(s => s.StatusName)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<SensorTicket?> GetTicketByIdAsync(int ticketId)
    {
        return await _context.SensorTickets
            .Include(t => t.Status)
            .Include(t => t.Sensor)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    /// <inheritdoc/>
    public async Task<bool> AddTicketResponseAsync(TicketResponse response)
    {
        try
        {
            _context.TicketResponses.Add(response);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TicketResponse>> GetTicketResponsesAsync(int ticketId)
    {
        return await _context.TicketResponses
                    .Include(r => r.Status) 
                    .Where(r => r.TicketId == ticketId)
                    .OrderBy(r => r.CreatedAt)
                    .ToListAsync();
    }

    /// <inheritdoc/>
    public event Action<int>? TicketDeleted;

    /// <inheritdoc/>
    public async Task<bool> DeleteTicketAsync(int ticketId)
    {
        try
        {
            var ticket = await _context.SensorTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            _context.SensorTickets.Remove(ticket);
            await _context.SaveChangesAsync();

            TicketDeleted?.Invoke(ticketId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] DeleteTicketAsync: {ex.Message}");
            return false;
        }
    }
}