using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying sensor reports and filtering sensors by type.
/// </summary>
public partial class SensorReportViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISensorService _sensorService;
    private readonly ISensorFilterService _filterService;
    private readonly INavigationService _navigationService;

    /// <summary>
    /// Collection of sensors to display in the report.
    /// </summary>
    public ObservableCollection<Sensor> Sensors { get; } = new();

    /// <summary>
    /// Collection of filter options for sensor types.
    /// </summary>
    public ObservableCollection<SensorFilter> FilterOptions { get; } = new();

    /// <summary>
    /// The currently selected filter for sensor types.
    /// </summary>
    [ObservableProperty]
    private SensorFilter selectedFilter;

    /// <summary>
    /// Indicates whether data is currently being loaded.
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorReportViewModel"/> class.
    /// </summary>
    /// <param name="sensorService">The sensor service for retrieving sensor data.</param>
    /// <param name="filterService">The filter service for filtering sensors.</param>
    /// <param name="navigationService">The navigation service for page navigation.</param>
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

    /// <summary>
    /// Applies query attributes when navigating to this page.
    /// </summary>
    /// <param name="query">The query parameters passed during navigation.</param>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted") || query.ContainsKey("saved") || query.ContainsKey("created"))
        {
            RefreshSensorList();
        }
        query.Clear();
    }

    /// <summary>
    /// Initializes the filter options for sensor types.
    /// </summary>
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

    /// <summary>
    /// Handles changes to the selected filter.
    /// </summary>
    /// <param name="value">The newly selected filter.</param>
    partial void OnSelectedFilterChanged(SensorFilter value)
    {
        if (value != null)
        {
            RefreshSensorList();
        }
    }
    
    /// <summary>
    /// Refreshes the list of sensors based on the selected filter.
    /// </summary>
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

    /// <summary>
    /// Loads the list of sensors.
    /// </summary>
    [RelayCommand]
    private void LoadSensors()
    {
        RefreshSensorList();
    }

    /// <summary>
    /// Navigates to the trend report page for a specific sensor.
    /// </summary>
    /// <param name="sensor">The sensor to generate a report for.</param>
    [RelayCommand]
    private async Task GenerateReport(Sensor sensor)
    {
        if (sensor == null) return;
        
        await _navigationService.NavigateToTrendReportAsync(sensor);
    }
}
