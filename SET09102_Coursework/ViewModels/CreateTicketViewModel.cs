using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System;
using System.Threading.Tasks;

namespace SET09102_Coursework.ViewModels;

[QueryProperty(nameof(Sensor), "Sensor")]
public partial class CreateTicketViewModel : ObservableObject
{
    private readonly ITicketService _ticketService;
    private readonly INavigationService _navigationService;

    [ObservableProperty] private Sensor sensor;
    [ObservableProperty] private string issueDescription;
    [ObservableProperty] private string errorMessage;
    [ObservableProperty] private bool hasError;

    public CreateTicketViewModel(ITicketService ticketService, INavigationService navigationService)
    {
        _ticketService = ticketService;
        _navigationService = navigationService;
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

        try
        {
            await _ticketService.CreateTicketAsync(ticket);
            await Shell.Current.DisplayAlert("Ticket Created", "Your issue has been logged.", "OK");
            await _navigationService.NavigateToSensorDetailsAsync(Sensor);
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
