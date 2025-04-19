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

    public AllSensorsViewModel(
        ISensorService sensorService,
        INavigationService navigationService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;

        InitializeFilterOptionsAsync().ConfigureAwait(false);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("refresh"))
        {
            LoadSensorsAsync().ConfigureAwait(false);
            query.Clear();
        }
    }

    [RelayCommand]
    private async Task ViewSensorDetails(Sensor sensor)
    {
        if (sensor == null) return;
        await _navigationService.NavigateToSensorDetailsAsync(sensor);
    }

    private async Task InitializeFilterOptionsAsync()
    {
        FilterOptions.Clear();
        FilterOptions.Add(new SensorFilter { SelectedTypeId = null, DisplayName = "All" });

        var sensorTypes = await _sensorService.GetSensorTypesAsync();
        foreach (var type in sensorTypes)
        {
            FilterOptions.Add(new SensorFilter 
            { 
                SelectedTypeId = type.Id, 
                DisplayName = type.Name 
            });
        }

        SelectedFilter = FilterOptions.First();
    }

    partial void OnSelectedFilterChanged(SensorFilter value)
    {
        LoadSensorsAsync().ConfigureAwait(false);
    }

    private async Task LoadSensorsAsync()
    {
        if (SelectedFilter == null) return;

        var sensorList = await _sensorService.GetSensorsByTypeAsync(SelectedFilter.SelectedTypeId);
        
        Sensors.Clear();
        foreach (var sensor in sensorList)
        {
            Sensors.Add(sensor);
        }
    }
}
