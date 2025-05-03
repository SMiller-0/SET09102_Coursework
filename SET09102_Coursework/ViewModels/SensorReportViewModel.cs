using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

public partial class SensorReportViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISensorService _sensorService;
    private readonly ISensorFilterService _filterService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Sensor> Sensors { get; } = new();
    public ObservableCollection<SensorFilter> FilterOptions { get; } = new();

    [ObservableProperty]
    private SensorFilter selectedFilter;

    [ObservableProperty]
    private bool isLoading;

    public SensorReportViewModel(
        ISensorService sensorService,
        ISensorFilterService filterService,
        INavigationService navigationService)
    {
        _sensorService = sensorService;
        _filterService = filterService;
        _navigationService = navigationService;
        
        InitializeFilterOptions();
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted") || query.ContainsKey("saved") || query.ContainsKey("created"))
        {
            RefreshSensorList();
        }
        query.Clear();
    }

    private async Task InitializeFilterOptions()
    {
        var types = await _sensorService.GetSensorTypesAsync();
        var filters = _filterService.GetTypeFilterOptions(types);
        
        FilterOptions.Clear();
        foreach (var filter in filters)
        {
            FilterOptions.Add(filter);
        }
        
        if (FilterOptions.Count > 0)
        {
            SelectedFilter = FilterOptions.First();
        }
    }

    partial void OnSelectedFilterChanged(SensorFilter value)
    {
        if (value != null)
        {
            RefreshSensorList();
        }
    }
    
    [RelayCommand]
    private async Task RefreshSensorList()
    {
        try
        {
            IsLoading = true;
            var sensors = await _sensorService.GetSensorsByTypeAsync(null);
            var filtered = _filterService.ApplyTypeFilter(sensors, SelectedFilter);
            
            Sensors.Clear();
            foreach (var s in filtered)
            {
                Sensors.Add(s);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error refreshing sensors: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void LoadSensors()
    {
        RefreshSensorList();
    }

    [RelayCommand]
    private async Task GenerateReport(Sensor sensor)
    {
        if (sensor == null) return;
        
        await _navigationService.NavigateToTrendReportAsync(sensor);
    }
}
