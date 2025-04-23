using System;

namespace SET09102_Coursework.Services;

public interface ITicketService
{

    /// <summary>
    /// Creates a new ticket for a sensor. This method is used to log issues or requests related to a specific sensor.
    /// The ticket will be created with the status "Open" by default.
    /// </summary>
    /// <param name="ticket">The ticket to create.</param>
    /// <returns>True if creation succeeds; otherwise, false.</returns>
    Task<bool> CreateTicketAsync(SensorTicket ticket);

    /// <summary>
    /// Retrieves a ticket by its ID. This method is used to fetch the details of a specific ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the sensor to retrieve tickets for.</param> 
    /// <returns>A list of matching tickets.</returns>
    Task<IEnumerable<SensorTicket>> GetTicketsBySensorAsync(int sensorId);

    /// <summary>
    /// Retrieves all tickets. This method is used to fetch the details of all tickets in the system.
    /// </summary>
    /// <returns>A list of all tickets is returned.</returns>
    Task<IEnumerable<SensorTicket>> GetAllTicketsAsync();

    /// <summary>
    /// Updates the status of a ticket. This method is used to change the status of a specific ticket.
    /// /// </summary>
    /// <param name="ticketId">The ID of the ticket to update.</param>
    /// <param name="newStatus">The new status (e.g. Closed, In Progress).</param>
    /// <returns>True if the update succeeds; otherwise, false.</returns>
    Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatus);

}
