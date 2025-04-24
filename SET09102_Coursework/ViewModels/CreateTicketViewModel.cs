using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
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


    [ObservableProperty] private Sensor sensor;
    [ObservableProperty] private string issueDescription;
    [ObservableProperty] private string errorMessage;
    [ObservableProperty] private bool hasError;

    /// <summary>
    /// Initialises the viewmodel and sets up the ticket service and navigation service.
    /// </summary>
    public CreateTicketViewModel(ITicketService ticketService, INavigationService navigationService, ITicketValidator ticketValidator)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;
        _ticketValidator = ticketValidator;
    }

    
    [RelayCommand]
    private async Task Submit()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(IssueDescription))
        {
            ErrorMessage = "Please provide a description of the issue.";
            HasError = true;
            return;
        }

        var openStatus = await _ticketService.GetStatusByNameAsync("Open");
        if (openStatus == null)
        {
            ErrorMessage = "Could not find 'Open' status in the system.";
            HasError = true;
            return;
        }

        var ticket = new SensorTicket
        {
            SensorId = Sensor.Id,
            IssueDescription = IssueDescription.Trim(),
            StatusId = openStatus.Id,
            CreatedAt = DateTime.UtcNow
        };

        var result = _ticketValidator.Validate(ticket);

        if (!result.IsValid)
        {
            ErrorMessage = result.ErrorMessage;
            HasError = true;
            return;
        }

        try
        {
            await _ticketService.CreateTicketAsync(ticket);
            await Shell.Current.DisplayAlert("Ticket Created", "Your issue has been logged.", "OK");
            await Shell.Current.GoToAsync("..");

        }
        catch (Exception ex)
        {
            ErrorMessage = $"Could not create ticket: {ex.Message}";
            HasError = true;
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
