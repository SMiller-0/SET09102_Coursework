using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;

namespace SET09102_Coursework.Services;

/// <summary>
/// Service for managing sensor tickets and their statuses.
/// </summary>
public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new ticket for a sensor. This method is used to log issues or requests related to a specific sensor.
    /// The ticket will be created with the status "Open" by default.
    /// </summary>
    /// <param name="ticket">The ticket to create.</param>
    /// <returns>True if creation succeeds; otherwise, false.</returns>
    /// <remarks>Note: The ticket is expected to have a valid sensor ID and status ID.</remarks>
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

    /// <summary>
    /// Retrieves a ticket by its ID. This method is used to fetch the details of a specific ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the sensor to retrieve tickets for.</param>
    /// <returns>A list of matching tickets.</returns>
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

    /// <summary>
    /// Retrieves all tickets. This method is used to fetch the details of all tickets in the system.
    /// </summary>
    /// <returns>A list of all tickets is returned.</returns>
    /// <remarks>Note: The tickets are ordered by status and then by creation date.</remarks>
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

    /// <summary>
    /// Updates the status of a ticket. This method is used to change the status of a specific ticket.
    /// </summary>
    /// <returns>True if the update succeeds; otherwise, false.</returns>
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

    /// <summary>
    /// Retrieves tickets by their status. This method is used to fetch tickets that match a specific status.
    /// </summary>
    /// <param name="statusId">The ID of the status to filter tickets by.</param>
    /// <returns>A list of tickets that match the specified status.</returns>
    /// <remarks>
    /// - The status ID corresponds to the specific <see cref="TicketStatus"/> type.
    /// - Tickets are ordered by creation date.
    /// - Tickets can be filtered by status ID.
    /// - Each ticket includes its `Status` and `Sensor` information.
    /// </remarks>
    public async Task<IEnumerable<SensorTicket>> GetTicketsByStatusAsync(int statusId)
    {
        return await _context.SensorTickets
            .Include(t => t.Status)
            .Include(t => t.Sensor)
            .Where(t => t.StatusId == statusId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all ticket statuses. This method is used to fetch the list of all possible ticket statuses.
    /// </summary>
    /// <returns>A list of all ticket statuses.</returns>
    /// <remarks>
    /// - The statuses are ordered by their name.
    /// - Each status is represented by its `Id` and `StatusName` properties.
    /// </remarks>
    public async Task<IEnumerable<TicketStatus>> GetAllTicketStatusesAsync()
    {
        return await _context.TicketStatuses
            .OrderBy(s => s.StatusName)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a ticket by its ID. This method is used to fetch the details of a specific ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to retrieve.</param>
    /// <returns>The ticket with the specified ID, including its status and sensor information.</returns>
    public async Task<SensorTicket?> GetTicketByIdAsync(int ticketId)
    {
        return await _context.SensorTickets
            .Include(t => t.Status)
            .Include(t => t.Sensor)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    /// <summary>
    /// Adds a response to a ticket. This method is used to log responses or updates related to a specific ticket.
    /// </summary>
    /// <param name="response">The response to add.</param>
    /// <returns>True if the addition succeeds; otherwise, false.</returns>
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

    public async Task<IEnumerable<TicketResponse>> GetTicketResponsesAsync(int ticketId)
    {
        return await _context.TicketResponses
                    .Where(r => r.TicketId == ticketId)
                    .OrderBy(r => r.CreatedAt)
                    .ToListAsync();
    }
    
}