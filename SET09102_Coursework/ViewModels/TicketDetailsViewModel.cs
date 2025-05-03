using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls; 
using System.Collections.ObjectModel;
using System.Linq;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying details of a single SensorTicket.
/// </summary> 
[QueryProperty(nameof(TicketId), "ticketId")]
public partial class TicketDetailsViewModel : ObservableObject
{
    private readonly ITicketService _ticketService;

    [ObservableProperty] 
    private int ticketId;

    [ObservableProperty] 
    private SensorTicket? ticket;

    /// <summary>All possible statuses for the dropdown</summary>
    public ObservableCollection<TicketStatus> Statuses { get; } = new();

    [ObservableProperty]
    private TicketStatus selectedStatus;

    public TicketDetailsViewModel(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

        // called whenever TicketId is set from the Shell query.
    partial void OnTicketIdChanged(int value)
    {
        _ = LoadTicketAsync(value);
    }

    private async Task LoadTicketAsync(int id)
        {
            // 1) load statuses
        Statuses.Clear();
        var sts = await _ticketService.GetAllTicketStatusesAsync();
        foreach (var s in sts) Statuses.Add(s);

        // 2) load the ticket
        Ticket = await _ticketService.GetTicketByIdAsync(id);

        // 3) set the picker to current status
        if (Ticket != null)
        {
            SelectedStatus = Statuses
                .FirstOrDefault(s => s.Id == Ticket.StatusId)
                ?? Statuses.First();
        }
        }

[RelayCommand]
    private async Task UpdateStatusAsync()
    {
        if (Ticket == null) return;

        var ok = await _ticketService.ChangeTicketStatusAsync(
            Ticket.Id, SelectedStatus.Id);

        if (!ok)
        {
            await Shell.Current.DisplayAlert(
                "Error", "Failed to update status", "OK");
            return;
        }

        // refresh the ticket so UI shows new Status nav-prop
        Ticket = await _ticketService.GetTicketByIdAsync(Ticket.Id);
        await Shell.Current.DisplayAlert(
            "Updated", "Ticket status updated.", "OK");
    }
}
