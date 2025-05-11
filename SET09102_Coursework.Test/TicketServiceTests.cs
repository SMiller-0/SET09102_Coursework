using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services
{
    public class TicketService
    {
        private readonly AppDbContext _ctx;

        public TicketService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> CreateTicketAsync(SensorTicket ticket)
        {
            _ctx.SensorTickets.Add(ticket);
            var changes = await _ctx.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<IEnumerable<SensorTicket>> GetTicketsBySensorAsync(int sensorId)
        {
            return await _ctx.SensorTickets
                             .Where(t => t.SensorId == sensorId)
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
        }

        public async Task<IEnumerable<SensorTicket>> GetAllTicketsAsync()
        {
            return await _ctx.SensorTickets
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
        }

        public async Task<bool> ChangeTicketStatusAsync(int ticketId, int newStatusId)
        {
            var ticket = await _ctx.SensorTickets.FindAsync(ticketId);
            if (ticket == null) return false;
            ticket.StatusId = newStatusId;
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SensorTicket>> GetTicketsByStatusAsync(int statusId)
        {
            return await _ctx.SensorTickets
                             .Where(t => t.StatusId == statusId)
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
        }

        public async Task<IEnumerable<TicketStatus>> GetAllTicketStatusesAsync()
        {
            return await _ctx.TicketStatuses
                             .OrderBy(s => s.StatusName)
                             .ToListAsync();
        }

        public async Task<SensorTicket?> GetTicketByIdAsync(int ticketId)
        {
            return await _ctx.SensorTickets
                             .Include(t => t.Status)
                             .Include(t => t.Sensor)
                             .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<bool> AddTicketResponseAsync(TicketResponse response)
        {
            _ctx.TicketResponses.Add(response);
            var changes = await _ctx.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<IEnumerable<TicketResponse>> GetTicketResponsesAsync(int ticketId)
        {
            return await _ctx.TicketResponses
                             .Where(r => r.TicketId == ticketId)
                             .OrderBy(r => r.CreatedAt)
                             .ToListAsync();
        }

        public event Action<int>? TicketDeleted;

        public async Task<bool> DeleteTicketAsync(int ticketId)
        {
            var ticket = await _ctx.SensorTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            _ctx.SensorTickets.Remove(ticket);
            await _ctx.SaveChangesAsync();

            TicketDeleted?.Invoke(ticketId);
            return true;
        }
    }
}
