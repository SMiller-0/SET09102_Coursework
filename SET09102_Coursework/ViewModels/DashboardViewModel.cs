using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Views;

namespace SET09102_Coursework.ViewModels;

public partial class DashboardViewModel: ObservableObject
{
   [RelayCommand]
    private async Task ViewAllUsers()
    {
        await Shell.Current.GoToAsync(nameof(Views.AllUsersPage));
    }

    [RelayCommand]
    private async Task ViewAllSensors()
    {
        await Shell.Current.GoToAsync(nameof(Views.AllSensorsPage));
    }
}