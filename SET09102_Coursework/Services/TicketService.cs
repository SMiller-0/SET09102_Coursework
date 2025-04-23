using System;

namespace SET09102_Coursework.Services;

public class TicketService
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
        var statusPriority = new Dictionary<string, int>
    {
        { TicketStatus.Open, 2 },
        { TicketStatus.InProgress, 1 },
        { TicketStatus.Closed, 3 }
    };

    try
        {
            return await _context.SensorTickets
                .AsEnumerable()
                .OrderBy(t =>
                {
                    if (statusPriority.TryGetValue(t.Status, out int priority))
                    {
                        return priority;
                    }
                    else
                    {
                        Console.WriteLine($"[Warning] Unexpected ticket status: '{t.Status}' (Ticket ID: {t.Id})");
                        return 99;
                    }
                })
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to load tickets: {ex.Message}");
            return Enumerable.Empty<SensorTicket>();
        }
    }

    public async Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatus)
    {
        try
        {
            var ticket = await _context.SensorTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to update ticket status: {ex.Message}");
            return false;
        }
    }
}