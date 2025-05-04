using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying and filtering all sensor tickets,
/// and for navigating to ticket details.
/// </summary>
public partial class AllTicketsViewModel: ObservableObject
{
    private readonly ITicketService _ticketService;
    private readonly INavigationService _navigationService;
    private readonly ICurrentUserService _currentUserService;
    
    /// <summary>
    /// Collection of tickets to be displayed in the UI.
    /// </summary>
    public ObservableCollection<SensorTicket> Tickets { get; } = new();

    /// <summary>
    /// Collection of ticket statuses for filtering tickets.
    /// </summary>
    /// <remarks>Contains a default "All" status.</remarks>
    public ObservableCollection<TicketStatus> Statuses { get; } = new();

    /// <summary>
    /// The full, unfiltered set of tickets fetched from the backend.
    /// </summary>
    [ObservableProperty] 
    private ObservableCollection<SensorTicket> allTickets = new();

    /// <summary>
    /// The currently selected ticket status for filtering tickets.
    /// Setting this will reload tickets.
    /// </summary>
    [ObservableProperty]
    private TicketStatus selectedStatus;

    /// <summary>
    /// The text used for searching tickets by sensor name.
    /// Setting this will filter the displayed tickets.
    /// </summary>
    /// <remarks>Search is case-insensitive.</remarks>
    [ObservableProperty]
    private string searchText = string.Empty;
    
    /// <summary>
    /// True if the current user is an Operations Manager.
    /// Controls whether “Details” is visible.
    /// </summary>
    [ObservableProperty]
    private bool isOperationsManager;

    /// <summary>
    /// Initialises the ViewModel: sets up ticket filtering, handles delete events,
    /// and listens for changes in the user’s role.
    /// </summary>
    /// <param name="ticketService">Service for managing tickets.</param>
    /// <param name="navigationService">Service for handling navigation.</param>
    /// <param name="currentUserService">Service for managing the current user’s state.</param>
    public AllTicketsViewModel(ITicketService ticketService, INavigationService navigationService, ICurrentUserService currentUserService)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;
        _currentUserService = currentUserService;
        
        // Reload or refilter when status or search text changes
        PropertyChanged += async (s, e) =>
        {
            if (e.PropertyName == nameof(SelectedStatus))
                await LoadByStatusAsync();

            if (e.PropertyName == nameof(SearchText))
                ApplySearchFilter();
        };

        // Listen for changes in the current user’s role
        IsOperationsManager = _currentUserService.IsOperationsManager;
        _currentUserService.UserChanged += (_,__)=>
            IsOperationsManager = _currentUserService.IsOperationsManager;

        // initial load
        _ = InitializeAsync();
    }

    /// <summary>
    /// Initialises the ViewModel by loading all ticket statuses and the initial set of tickets.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    private async Task InitializeAsync()
    {
        // Load all statuses into the picker
        var sts = await _ticketService.GetAllTicketStatusesAsync();
        Statuses.Clear();
        Statuses.Add(new TicketStatus { Id = 0, StatusName = "All" });
        foreach (var st in sts) Statuses.Add(st);

        // default to "All"
        SelectedStatus = Statuses.First();

        // Load all tickets
        await LoadByStatusAsync();
    }

    /// <summary>
    /// Loads tickets based on the selected status.
    /// If "All" is selected, all tickets are loaded.
    /// If a specific status is selected, only tickets with that status are loaded.
    /// Also applies the current search filter to the loaded tickets.
    /// </summary>
    [RelayCommand]
    private async Task LoadByStatusAsync()
    {
        IEnumerable<SensorTicket> list = SelectedStatus.Id == 0
            ? await _ticketService.GetAllTicketsAsync()
            : await _ticketService.GetTicketsByStatusAsync(SelectedStatus.Id);

        allTickets.Clear();
        foreach (var t in list) allTickets.Add(t);

        ApplySearchFilter();
    }
    
    /// <summary>
    /// Applies the current search filter to the list of tickets.
    /// Filters tickets based on the sensor name.
    /// </summary>
    /// <remarks>Search is case-insensitive.</remarks>
    /// <remarks>Search is performed on the sensor name.</remarks>
    private void ApplySearchFilter()
    {
        var query = allTickets.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            query = query.Where(t =>
                t.Sensor.Name
                 .Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase));

        Tickets.Clear();
        foreach (var t in query) Tickets.Add(t);
    }

    /// <summary>
    /// Navigates to the ticket details page for the selected ticket.
    /// Invoked when the user clicks on the details button.
    /// Only visibile to those with the role Operations Manager.
    /// </summary>
    /// <param name="ticket">The ticket to view.</param>
    [RelayCommand]
    private async Task ViewTicketDetails(SensorTicket ticket)
    {
        if (ticket == null) return;
        await _navigationService.NavigateToTicketDetailsAsync(ticket);
    }

}
