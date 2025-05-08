using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for the Sensor Status page that displays the current status of all sensors.
/// Implements IDisposable to properly clean up the refresh service.
/// </summary>
public partial class SensorStatusViewModel : ObservableObject, IDisposable
{
    private readonly ISensorRefreshService _refreshService;
    private readonly ISensorFilterService _filterService;
    
    /// <summary>
    /// The interval in seconds between automatic sensor data refreshes.
    /// </summary>
    private const int REFRESH_INTERVAL_SECONDS = 5;

    /// <summary>
    /// Observable collection of sensors displayed in the UI.
    /// This collection is updated when filters are applied or data is refreshed.
    /// </summary>
    public ObservableCollection<Sensor> Sensors { get; } = new();
    
    /// <summary>
    /// Observable collection of filter options available in the UI.
    /// Populated during initialization from the filter service.
    /// </summary>
    public ObservableCollection<SensorStatusFilter> FilterOptions { get; } = new();

    /// <summary>
    /// Flag indicating whether a refresh operation is in progress.
    /// Used to control the refresh indicator in the UI.
    /// </summary>
    [ObservableProperty]
    private bool isRefreshing;

    /// <summary>
    /// The currently selected filter option.
    /// When changed, the filter is applied to the sensor collection.
    /// </summary>
    [ObservableProperty]
    private SensorStatusFilter selectedFilter;

    /// <summary>
    /// The complete collection of sensors before filtering.
    /// Updated when new sensor data is received from the refresh service.
    /// </summary>
    private IEnumerable<Sensor> _allSensors = new List<Sensor>();

    /// <summary>
    /// Initializes a new instance of the SensorStatusViewModel class.
    /// Sets up event handlers and initializes data.
    /// </summary>
    /// <param name="refreshService">Service for refreshing sensor data</param>
    /// <param name="filterService">Service for filtering sensor data</param>
    public SensorStatusViewModel(
        ISensorRefreshService refreshService,
        ISensorFilterService filterService)
    {
        _refreshService = refreshService;
        _filterService = filterService;
        
        // Subscribe to the refresh event
        _refreshService.SensorsRefreshed += OnSensorsRefreshed;
        
        // Initialize filter options and start data loading
        InitializeFilters();
        InitializeAsync();
    }

    /// <summary>
    /// Initializes the filter options from the filter service.
    /// Sets the default selected filter to the first option.
    /// </summary>
    private void InitializeFilters()
    {
        FilterOptions.Clear();
        var filters = _filterService.GetStatusFilterOptions();
        foreach (var filter in filters)
        {
            FilterOptions.Add(filter);
        }
        SelectedFilter = FilterOptions.First();
    }

    /// <summary>
    /// Handler for when the selected filter changes.
    /// Applies the new filter to the sensor collection.
    /// </summary>
    /// <param name="value">The newly selected filter</param>
    partial void OnSelectedFilterChanged(SensorStatusFilter value)
    {
        if (value != null)
        {
            ApplyFilter();
        }
    }

    /// <summary>
    /// Applies the currently selected filter to the sensor collection.
    /// Updates the UI on the main thread to avoid cross-thread access issues.
    /// </summary>
    private void ApplyFilter()
    {
        var filteredSensors = _filterService.ApplyStatusFilter(_allSensors, SelectedFilter);
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Sensors.Clear();
            foreach (var sensor in filteredSensors)
            {
                Sensors.Add(sensor);
            }
        });
    }

    /// <summary>
    /// Initializes the ViewModel asynchronously.
    /// Loads initial sensor data and starts the auto-refresh timer.
    /// </summary>
    private async void InitializeAsync()
    {
        await LoadSensors();
        _refreshService.StartAutoRefresh(REFRESH_INTERVAL_SECONDS);
    }

    /// <summary>
    /// Event handler for when sensors are refreshed.
    /// Updates the internal sensor collection and applies the current filter.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="sensors">The refreshed collection of sensors</param>
    private void OnSensorsRefreshed(object? sender, IEnumerable<Sensor> sensors)
    {
        _allSensors = sensors;
        ApplyFilter();
    }

    /// <summary>
    /// Command to manually refresh the sensor data.
    /// Sets the IsRefreshing flag during the operation.
    /// </summary>
    [RelayCommand]
    private async Task LoadSensors()
    {
        try
        {
            IsRefreshing = true;
            await _refreshService.RefreshSensors();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    /// <summary>
    /// Disposes of resources used by the ViewModel.
    /// Unsubscribes from events and disposes the refresh service.
    /// </summary>
    public void Dispose()
    {
        _refreshService.Dispose();
        GC.SuppressFinalize(this);
    }
}



