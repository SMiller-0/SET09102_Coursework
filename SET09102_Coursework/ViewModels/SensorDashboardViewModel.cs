using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public partial class SensorDashboardViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public SensorDashboardViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task ViewSensors()
    {
        await _navigationService.NavigateToAllSensorsAsync();
    }

    [RelayCommand]
    private async Task AddNewSensor()
    {
        await _navigationService.NavigateToAddNewSensorAsync();
    }
}



