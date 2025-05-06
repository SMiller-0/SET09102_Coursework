using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Validation;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for creating a new ticket.
/// Handles input fields, validation, and ticket creation via the ticket service.
/// </summary>
[QueryProperty(nameof(Sensor), "Sensor")]
public partial class CreateTicketViewModel : ObservableObject
{
    private readonly ITicketService _ticketService;
    private readonly INavigationService _navigationService;
    private readonly ITicketValidator _ticketValidator;


    /// <summary>
    // Sensor associated with the ticket.
    // </summary>
    [ObservableProperty] 
    private Sensor sensor;

    /// <summary>
    /// Description of the issue entered by the user.
    /// </summary>
    [ObservableProperty] 
    private string issueDescription;

    [ObservableProperty] 
    private string errorMessage;
    
    [ObservableProperty] 
    private bool hasError;

    /// <summary>
    /// List of ticket statuses (e.g. Open, Closed) used for display or assignment.
    /// </summary>
    /// <remarks>Includes a default "All" status.</remarks>
    [ObservableProperty] 
    private ObservableCollection<TicketStatus> statuses = new();


    /// <summary>
    /// Initialises the viewmodel and sets up the ticket, ticketValidator, and navigation service.
    /// </summary>
    /// <param name="ticketService">Service for ticket operations.</param>
    /// <param name="navigationService">Service for navigation operations.</param>
    /// <param name="ticketValidator">Validator for ticket input.</param>
    public CreateTicketViewModel(ITicketService ticketService, INavigationService navigationService, ITicketValidator ticketValidator)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;
        _ticketValidator = ticketValidator;

        _ = InitializeAsync();
    }


    /// <summary>
    /// Loads all ticket statuses into the viewmodel for selection.
    /// </summary>
    private async Task InitializeAsync()
    {
        var sts = await _ticketService.GetAllTicketStatusesAsync();
        statuses.Clear();
        foreach (var s in sts)
            statuses.Add(s);
    }

    /// <summary>
    /// Submits the ticket to the service after validating the input.
    /// Gives feedback to the user on success or failure. Then navigates back to the previous page.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    [RelayCommand]
    private async Task Submit()
    {
        // Quick validation
        if (string.IsNullOrWhiteSpace(IssueDescription))
        {
            ErrorMessage = "Please provide a description of the issue.";
            HasError     = true;
            return;
        }

        var open = Statuses.First(s => s.StatusName == "Open");

        // Construct the ticket
        var ticket = new SensorTicket
        {
            SensorId         = Sensor.Id,
            IssueDescription = IssueDescription.Trim(),
            StatusId         = open.Id,
            CreatedAt        = DateTime.UtcNow
        };

        // Validator
        var result = _ticketValidator.Validate(ticket);
        if (!result.IsValid)
        {
            ErrorMessage = result.ErrorMessage;
            HasError     = true;
            return;
        }

        // Attempt to create the ticket
        try
        {
            await _ticketService.CreateTicketAsync(ticket);
            await Shell.Current.DisplayAlert("Ticket Created", "Your issue has been logged.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Could not create ticket: {ex.Message}";
            HasError     = true;
        }
    }


    /// <summary>
    /// Cancels ticket creation and navigates back to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
