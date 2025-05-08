using SET09102_Coursework.Models;
using System.Collections.Generic;

namespace SET09102_Coursework.Services;

/// <summary>
/// Defines operations for creating and retrieving sensor tickets and their statuses.
/// </summary>
public interface ITicketService
{

    /// <summary>
    /// Creates a new ticket for a sensor. The ticket will default to status “Open.”
    /// </summary>
    /// <param name="ticket">The <see cref="SensorTicket"/> to insert.</param>    
    /// <returns>
    /// <c>true</c> if the insert succeeds; otherwise <c>false</c>.
    /// </returns>
    Task<bool> CreateTicketAsync(SensorTicket ticket);

    /// <summary>
    /// Retrieves all tickets for a given sensor.
    /// </summary>
    /// <param name="sensorId">The ID of the sensor whose tickets to fetch.</param>    
    /// <returns>
    /// A collection of <see cref="SensorTicket"/> belonging to that sensor.
    /// </returns>
    Task<IEnumerable<SensorTicket>> GetTicketsBySensorAsync(int sensorId);

    /// <summary>
    /// Retrieves all tickets in the system, ordered by status and date.
    /// </summary>
    /// <returns>
    /// A collection of all <see cref="SensorTicket"/>.
    /// </returns>
    Task<IEnumerable<SensorTicket>> GetAllTicketsAsync();

    /// <summary>
    /// Retrieves tickets matching the specified status.
    /// </summary>
    /// <param name="statusId">The status ID to filter by (e.g. Open, Closed).</param>
    /// <returns>
    /// A collection of <see cref="SensorTicket"/> with that status.
    /// </returns>
    Task<IEnumerable<SensorTicket>> GetTicketsByStatusAsync(int statusId);

    /// <summary>
    /// Fetches the full list of possible ticket statuses.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="TicketStatus"/>.
    /// </returns>
    Task<IEnumerable<TicketStatus>> GetAllTicketStatusesAsync();

    /// <summary>
    /// Retrieves a single ticket by its ID, including its sensor and status.
    /// </summary>
    /// <param name="ticketId">The ticket’s unique identifier.</param>
    /// <returns>
    /// The matching <see cref="SensorTicket"/>, or <c>null</c> if not found.
    /// </returns>
    Task<SensorTicket?> GetTicketByIdAsync(int ticketId);

    /// <summary>
    /// Changes the status of the given ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to update.</param>
    /// <param name="newStatusId">The ID of the new status.</param>
    /// <returns>
    /// <c>true</c> if the update succeeds; otherwise <c>false</c>.
    /// </returns>
    Task<bool> ChangeTicketStatusAsync(int ticketId, int newStatusId);

    /// <summary>
    /// Logs a new response (status change + optional note) to a ticket.
    /// </summary>
    /// <param name="response">The <see cref="TicketResponse"/> to record.</param>
    /// <returns>
    /// <c>true</c> if the response is logged successfully; otherwise <c>false</c>.
    /// </returns>
    Task<bool> AddTicketResponseAsync(TicketResponse response);

    /// <summary>
    /// Fetches the history of all responses for a ticket, in chronological order.
    /// </summary>
    /// <param name="ticketId">The ticket’s unique identifier.</param>
    /// <returns>
    /// A collection of <see cref="TicketResponse"/> sorted oldest-first.
    /// </returns>
    Task<IEnumerable<TicketResponse>> GetTicketResponsesAsync(int ticketId);

    /// <summary>
    /// Deletes a ticket and all its associated responses.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to remove.</param>
    /// <returns>
    /// <c>true</c> if deletion succeeds; otherwise <c>false</c>.
    /// </returns>
    Task<bool> DeleteTicketAsync(int ticketId);

    /// <summary>
    /// Raised whenever a ticket is deleted (passes the deleted ticket’s ID).
    /// </summary>
    event Action<int> TicketDeleted;
}
