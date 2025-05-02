using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying details of a single SensorTicket.
/// </summary> 
//[QueryProperty(nameof(TicketId), "ticketId")]
public partial class TicketDetailsViewModel //: ObservableObject, IQueryAttributable
{
/*     private readonly INavigationService _navigationService;
    private readonly ITicketService _ticketService;

    /// <summary>The ID passed in via Shell navigation query.</summary>
    [ObservableProperty] private int ticketId;
    /// <summary>The loaded ticket, once fetched.</summary>
    [ObservableProperty] private SensorTicket? ticket;


    public TicketDetailsViewModel(ITicketService ticketService, INavigationService navigationService)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ticketId", out var raw) &&
                int.TryParse(raw?.ToString(), out var id))
            {
                TicketId = id;
                _ = LoadTicketAsync();
            }
        }
 */
        /// <summary>
        /// Pulls the SensorTicket from the service using TicketId.
        /// </summary>
       // private async Task LoadTicketAsync()
       //{
         //   Ticket = await _ticketService.GetTicketByIdAsync(TicketId);
       // }
//

    

}
