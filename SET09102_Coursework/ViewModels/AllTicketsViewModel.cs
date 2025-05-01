using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

public partial class AllTicketsViewModel: ObservableObject
{
    private readonly ITicketService _ticketService;
    private readonly INavigationService _navigationService;

    [ObservableProperty] 
    private ObservableCollection<SensorTicket> allTickets = new();

    public AllTicketsViewModel(ITicketService ticketService, INavigationService navigationService)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;

        LoadTickets().ConfigureAwait(false);
    }

    [RelayCommand]
    private async Task LoadTickets()
    {
        try
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            
            AllTickets.Clear();
            foreach (var ticket in tickets)
            {
                AllTickets.Add(ticket);
            }
        
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to load tickets: {ex.Message}");
        }
        
    }
    

}
