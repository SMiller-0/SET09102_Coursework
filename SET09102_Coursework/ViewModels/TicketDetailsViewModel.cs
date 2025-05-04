using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying and managing ticket details.
/// Loads a single ticket, its possible statuses, and its response history,
/// and provides commands to save updates or delete the ticket.
[QueryProperty(nameof(TicketId), "ticketId")]
public partial class TicketDetailsViewModel : ObservableObject
{
    private readonly ITicketService _ticketService;

    /// <summary>
    /// The ID of the ticket being viewed.
    /// </summary>
    [ObservableProperty] 
    private int ticketId;

    /// <summary>
    /// The currently loaded ticket, including its sensor and status.
    /// </summary>
    [ObservableProperty] 
    private SensorTicket? ticket;

    /// <summary>
    /// The collection of all possible ticket statuses.
    /// </summary>
    /// <remarks>Includes a default "All" status.</remarks>
    public ObservableCollection<TicketStatus> Statuses { get; } = new();

    /// <summary>
    /// The status selected by the user in the UI.
    /// </summary>
    [ObservableProperty] 
    private TicketStatus selectedStatus;

    /// <summary>
    /// The note text entered by the user when saving a response.
    /// </summary>
    [ObservableProperty] 
    private string note = string.Empty;

    /// <summary>
    /// The history of responses logged for this ticket.
    /// </summary>
    public ObservableCollection<TicketResponse> Responses { get; } = new();

    /// <summary>
    /// Event handler for when the ticket ID changes.
    /// This triggers loading of the ticket details and history.
    /// </summary>
    partial void OnTicketIdChanged(int value) 
        => _ = LoadAllAsync(value);

    /// <summary>
    /// Initialises the ticket details ViewModel.
    /// </summary>
    /// <param name="ticketService">The service for managing tickets.</param>
    public TicketDetailsViewModel(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    /// <summary>
    /// Loads ticket details, all statuses, and response history.
    /// <param name="id">The ticket ID to load.</param>
    /// <returns>A task that completes when loading is finished.</returns>
    private async Task LoadAllAsync(int id)
    {
        // load statuses
        Statuses.Clear();
        foreach (var s in await _ticketService.GetAllTicketStatusesAsync())
            Statuses.Add(s);

        // load ticket and select its current status
        Ticket = await _ticketService.GetTicketByIdAsync(id);
        if (Ticket != null)
            SelectedStatus = Statuses.First(s => s.Id == Ticket.StatusId);

        // load response log
        Responses.Clear();
        foreach (var r in await _ticketService.GetTicketResponsesAsync(id))
            Responses.Add(r);
    }

    /// <summary>
    /// Saves any status change and always logs a new <see cref="TicketResponse"/>.
    /// </summary>
    /// <returns>A task that completes when saving is done.</returns>
    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Ticket == null) return;

        // 1) Did status change?
        if (Ticket.StatusId != SelectedStatus.Id)
        {
            var ok = await _ticketService.ChangeTicketStatusAsync(
                Ticket.Id, SelectedStatus.Id);
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Error", 
                    "Failed to update status", "OK");
                return;
            }
            Ticket.StatusId = SelectedStatus.Id;
        }

        // 2) Always log a response (even if Note blank)
        var resp = new TicketResponse
        {
            TicketId  = Ticket.Id,
            StatusId  = SelectedStatus.Id,
            Note      = Note.Trim(),
            CreatedAt = DateTime.UtcNow
        };
        if (! await _ticketService.AddTicketResponseAsync(resp))
        {
            await Shell.Current.DisplayAlert("Error", 
                "Failed to log response", "OK");
            return;
        }

        // 3) reload everything
        Note = string.Empty;
        await LoadAllAsync(Ticket.Id);
        await Shell.Current.DisplayAlert("Saved", 
            "Status & response recorded.", "OK");
    }

    /// <summary>
    /// Deletes the current ticket (and cascades its responses).
    /// Asks the user to confirm before proceeding.
    /// </summary>
    /// <param name="ticket">Ignored; uses the bound <see cref="Ticket"/>.</param>
    [RelayCommand]
    private async Task DeleteTicketAsync(SensorTicket ticket)
    {
        if (Ticket == null) return;

        bool confirmed = await Shell.Current.DisplayAlert(
            "Confirm Delete",
            $"Delete ticket #{Ticket.Id} and all its responses?",
            "Delete", "Cancel");
        if (!confirmed) return;

        var success = await _ticketService.DeleteTicketAsync(Ticket.Id);
        if (!success)
        {
            await Shell.Current.DisplayAlert("Error",
             "Could not delete the ticket. Please try again.", "OK");
            return;
        }

     await Shell.Current.Navigation.PopAsync();
    }
}
