using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls; 
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

    /// <summary>The ID passed in via Shell navigation query.</summary>
    [ObservableProperty] private int ticketId;
    /// <summary>The loaded ticket, once fetched.</summary>
    [ObservableProperty] private SensorTicket ticket;


    public TicketDetailsViewModel(ITicketService ticketService, INavigationService navigationService)
    {
        _ticketService = ticketService;
    }


    private async Task LoadTicketAsync(int id)
        {
            Ticket = await _ticketService.GetTicketByIdAsync(id);
        }

        partial void OnTicketIdChanged(int value)
        {
            _ = LoadTicketAsync(value);
        }

       [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
}
