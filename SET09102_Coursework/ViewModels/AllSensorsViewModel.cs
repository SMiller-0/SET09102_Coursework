using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for the All Sensors page that displays and manages the list of sensors.
/// Implements IQueryAttributable to handle navigation parameters.
/// </summary>
public partial class AllSensorsViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;
    private readonly ISensorFilterService _filterService;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Collection of sensors to display in the UI.
    /// Updated when filters are applied or data is refreshed.
    /// </summary>
    public ObservableCollection<Sensor> Sensors { get; } = new();
    
    /// <summary>
    /// Collection of filter options for sensor types.
    /// Populated during initialization from the filter service.
    /// </summary>
    public ObservableCollection<SensorFilter> FilterOptions { get; } = new();
    
    /// <summary>
    /// Command to navigate to the sensor details page.
    /// Takes a Sensor object as parameter.
    /// </summary>
    public ICommand ViewSensorDetailsCommand { get; }

    /// <summary>
    /// The currently selected filter option.
    /// When changed, the filter is applied to the sensor collection.
    /// </summary>
    [ObservableProperty]
    private SensorFilter selectedFilter;

    /// <summary>
    /// Flag indicating whether the current user has admin privileges.
    /// Controls visibility of admin-only features in the UI.
    /// </summary>
    public bool IsAdmin => _currentUserService.IsAdmin;

    /// <summary>
    /// Initializes a new instance of the AllSensorsViewModel class.
    /// Sets up commands, event handlers, and initializes data.
    /// </summary>
    /// <param name="sensorService">Service for sensor operations</param>
    /// <param name="navigationService">Service for navigation between pages</param>
    /// <param name="filterService">Service for filtering sensors</param>
    /// <param name="currentUserService">Service for current user information</param>
    public AllSensorsViewModel(
        ISensorService sensorService,
        INavigationService navigationService,
        ISensorFilterService filterService,
        ICurrentUserService currentUserService)
    {
        _sensorService = sensorService;
        _navigationService = navigationService;
        _filterService = filterService;
        _currentUserService = currentUserService;
        _currentUserService.UserChanged += OnUserChanged;

        ViewSensorDetailsCommand = new AsyncRelayCommand<Sensor>(ViewSensorDetailsAsync);
        
        InitializeFilterOptions();
    }

    /// <summary>
    /// Event handler for when the current user changes.
    /// Updates the IsAdmin property to reflect the new user's permissions.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin));
    }

    /// <summary>
    /// Initializes the filter options for sensor types.
    /// Sets the default selected filter to the first option.
    /// </summary>
    private async void InitializeFilterOptions()
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
    /// Command to navigate to the Add Sensor page.
    /// Only available to users with admin privileges.
    /// </summary>
    /// <returns>A task representing the asynchronous operation</returns>
    [RelayCommand]
    private async Task AddSensor()
    {
        await _navigationService.NavigateToAddNewSensorAsync();
    }

    /// <summary>
    /// Navigates to the Sensor Details page for the selected sensor.
    /// </summary>
    /// <param name="sensor">The sensor to view details for</param>
    /// <returns>A task representing the asynchronous operation</returns>
    private async Task ViewSensorDetailsAsync(Sensor sensor)
    {
        if (sensor != null)
        {
            await _navigationService.NavigateToSensorDetailsAsync(sensor);
        }
    }

    /// <summary>
    /// Implements IQueryAttributable.ApplyQueryAttributes to handle navigation parameters.
    /// Refreshes the sensor list when returning from other pages with changes.
    /// </summary>
    /// <param name="query">Dictionary of query parameters</param>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted") || query.ContainsKey("saved") || query.ContainsKey("created"))
        {
            RefreshSensorList();
        }
    }

    /// <summary>
    /// Handler for when the selected filter changes.
    /// Refreshes the sensor list with the new filter applied.
    /// </summary>
    /// <param name="value">The newly selected filter</param>
    partial void OnSelectedFilterChanged(SensorFilter value)
    {
        if (value != null)
        {
            RefreshSensorList();
        }
    }
    
    /// <summary>
    /// Refreshes the list of sensors based on the selected filter.
    /// Fetches all sensors and applies the filter.
    /// </summary>
    /// <returns>A task representing the asynchronous operation</returns>
    [RelayCommand]
    private async Task RefreshSensorList()
    {
        try
        {
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
    }

    /// <summary>
    /// Command to load the list of sensors.
    /// Called when the page is navigated to.
    /// </summary>
    /// <returns>A task representing the asynchronous operation</returns>
    [RelayCommand]
    private async Task LoadSensors()
    {
        await RefreshSensorList();
    }
}
