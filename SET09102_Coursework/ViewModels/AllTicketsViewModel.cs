using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying all tickets.
/// Handles ticket loading, filtering by status, and navigation to ticket details page.
/// </summary>
public partial class AllTicketsViewModel: ObservableObject
{
    private readonly ITicketService _ticketService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<SensorTicket> Tickets { get; } = new();
    public ObservableCollection<TicketStatus> Statuses { get; } = new();


    [ObservableProperty] 
    private ObservableCollection<SensorTicket> allTickets = new();

    [ObservableProperty]
    private TicketStatus selectedStatus;


    public AllTicketsViewModel(ITicketService ticketService, INavigationService navigationService)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;

        // whenever the user picks a new status, reload that slice
        PropertyChanged += async (s, e) =>
        {
            if (e.PropertyName == nameof(SelectedStatus))
                await LoadByStatusAsync();
        };

        // initial load
        _ = InitializeAsync();
    }


    
    private async Task InitializeAsync()
    {
        // Load all statuses into the picker
        var sts = await _ticketService.GetAllTicketStatusesAsync();
        Statuses.Clear();
        Statuses.Add(new TicketStatus { Id = 0, StatusName = "All" });
        foreach (var st in sts) Statuses.Add(st);

        // default selection
        SelectedStatus = Statuses.First();

        // initial ticket load
        await LoadByStatusAsync();
    }


    /// <summary>
    /// Loads tickets based on the selected status.
    
    ///  
    /// If "All" is selected, all tickets are loaded.
    /// If a specific status is selected, only tickets with that status are loaded.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    
    [RelayCommand]
    private async Task LoadByStatusAsync()
    {
        IEnumerable<SensorTicket> list;
        if (SelectedStatus.Id == 0)
        {
            
            list = await _ticketService.GetAllTicketsAsync();
        }
        else
        {
            
            list = await _ticketService.GetTicketsByStatusAsync(SelectedStatus.Id);
        }

        Tickets.Clear();
        foreach (var t in list)
            Tickets.Add(t);
    }
    
    
    [RelayCommand]
    private async Task ViewTicketDetails(SensorTicket ticket)
    {
        if (ticket == null) return;
        await _navigationService.NavigateToTicketDetailsAsync(ticket);
    }

}
