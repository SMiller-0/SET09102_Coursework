using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

[QueryProperty(nameof(TicketId), "ticketId")]
public partial class TicketDetailsViewModel : ObservableObject
{
    private readonly ITicketService _ticketService;

    public TicketDetailsViewModel(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [ObservableProperty] private int ticketId;
    [ObservableProperty] private SensorTicket? ticket;
    public ObservableCollection<TicketStatus> Statuses { get; } = new();
    [ObservableProperty] private TicketStatus selectedStatus;
    [ObservableProperty] private string note = string.Empty;
    public ObservableCollection<TicketResponse> Responses { get; } = new();

    partial void OnTicketIdChanged(int value) 
        => _ = LoadAllAsync(value);

    private async Task LoadAllAsync(int id)
    {
        // load statuses
        Statuses.Clear();
        foreach (var s in await _ticketService.GetAllTicketStatusesAsync())
            Statuses.Add(s);

        // load ticket
        Ticket = await _ticketService.GetTicketByIdAsync(id);
        if (Ticket != null)
            SelectedStatus = Statuses.First(s => s.Id == Ticket.StatusId);

        // load history
        Responses.Clear();
        foreach (var r in await _ticketService.GetTicketResponsesAsync(id))
            Responses.Add(r);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Ticket == null) return;

        // 1) status change?
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

        // 2) always log a response (even if Note blank)
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

private async Task LoadResponsesAsync()
    {
        if (Ticket == null) return;
        Responses.Clear();
        var history = await _ticketService.GetTicketResponsesAsync(Ticket.Id);
        foreach (var r in history) Responses.Add(r);
    }

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
