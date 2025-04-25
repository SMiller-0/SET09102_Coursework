using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

public partial class AllSensorsViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Sensor> Sensors { get; } = new();
    public ObservableCollection<SensorFilter> FilterOptions { get; } = new();

    [ObservableProperty]
    private SensorFilter selectedFilter;

    partial void OnSelectedFilterChanged(SensorFilter value)
    {
        if (value != null)  // Add null check
        {
            LoadSensorsCommand.Execute(null);
        }
    }

    public AllSensorsViewModel(
        ISensorService sensorService,
        INavigationService navigationService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        LoadSensors().ConfigureAwait(false);
        query.Clear();
    }

    private async Task InitializeFilterOptionsAsync()
    {
        if (FilterOptions.Count == 0)  // Only initialize if not already initialized
        {
            var types = await _sensorService.GetSensorTypesAsync();
            FilterOptions.Clear();
            
            FilterOptions.Add(new SensorFilter 
            { 
                SelectedTypeId = null, 
                DisplayName = "All Sensors" 
            });

            foreach (var type in types)
            {
                FilterOptions.Add(new SensorFilter 
                { 
                    SelectedTypeId = type.Id, 
                    DisplayName = type.Name 
                });
            }

            SelectedFilter = FilterOptions.First();
        }
    }

    [RelayCommand]
    private async Task LoadSensors()
    {
        await InitializeFilterOptionsAsync();
        var sensorList = await _sensorService.GetSensorsByTypeAsync(SelectedFilter?.SelectedTypeId);
        
        Sensors.Clear();
        foreach (var sensor in sensorList)
        {
            Sensors.Add(sensor);
        }
    }

    [RelayCommand]
    private async Task ViewSensorDetails(Sensor sensor)
    {
        if (sensor == null) return;
        await _navigationService.NavigateToSensorDetailsAsync(sensor);
    }
}
