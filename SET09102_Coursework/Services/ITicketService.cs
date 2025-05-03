using SET09102_Coursework.Models;
using System.Collections.Generic;

namespace SET09102_Coursework.Services;

/// <summary>
/// Defines operations for creating and retrieving sensor tickets and their statuses.
/// </summary>
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
    /// <param name="sensorId">The ID of the sensor to retrieve tickets for.</param> 
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
    //Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatus);

    /// <summary>
    /// Retrieves tickets by their status. This method is used to fetch tickets that match a specific status.
    /// </summary>
    /// <param name="statusId">The ID of the status to filter tickets by.</param>
    /// <returns>A list of tickets that match the specified status.</returns>
    /// <remarks>Note: The status ID is typically an integer that corresponds to a specific status type.</remarks>
    Task<IEnumerable<SensorTicket>> GetTicketsByStatusAsync(int statusId);

    /// <summary>
    /// Retrieves all ticket statuses. This method is used to fetch the list of all possible ticket statuses.
    /// </summary>
    /// <returns>A list of all ticket statuses.</returns>
    Task<IEnumerable<TicketStatus>> GetAllTicketStatusesAsync();

    /// <summary>
    /// Retrieves a ticket by its ID. This method is used to fetch the details of a specific ticket status.
    /// /// </summary>
    /// <param name="ticketId">The ID of the status to retrieve.</param>
    /// <returns>The ticket status that matches the specified ID.</returns>
    Task<SensorTicket?> GetTicketByIdAsync(int ticketId);

    /// <summary>
    /// Change the status of a single ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to update.</param>
    /// <param name="newStatusId">The ID of the new status.</param>
    /// <returns>True if the update succeeds; otherwise false.</returns>
    Task<bool> ChangeTicketStatusAsync(int ticketId, int newStatusId);

    /// <summary>
    /// Logs a new response (status change + note) for the given ticket.
    /// </summary>
    /// <param name="response">The response to log.</param>
    /// <returns>True if the response is logged successfully; otherwise false.</returns>
    Task<bool> AddTicketResponseAsync(TicketResponse response);

    /// <summary>
    /// Fetches all responses/history entries for a ticket, ordered oldest first.
    /// </summary>
    Task<IEnumerable<TicketResponse>> GetTicketResponsesAsync(int ticketId);

}
