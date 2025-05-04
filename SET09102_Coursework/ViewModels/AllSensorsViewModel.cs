using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SET09102_Coursework.ViewModels;

public partial class AllSensorsViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISensorService _sensorService;
    private readonly INavigationService _navigationService;
    private readonly ISensorFilterService _filterService;
    private readonly ICurrentUserService _currentUserService;

    public ObservableCollection<Sensor> Sensors { get; } = new();
    public ObservableCollection<SensorFilter> FilterOptions { get; } = new();
    public ICommand ViewSensorDetailsCommand { get; }

    [ObservableProperty]
    private SensorFilter selectedFilter;

    public bool IsAdmin => _currentUserService.IsAdmin;

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

    private void OnUserChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(IsAdmin));
    }

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

    [RelayCommand]
    private async Task AddSensor()
    {
        await _navigationService.NavigateToAddNewSensorAsync();
    }

    private async Task ViewSensorDetailsAsync(Sensor sensor)
    {
        if (sensor != null)
        {
            await _navigationService.NavigateToSensorDetailsAsync(sensor);
        }
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted") || query.ContainsKey("saved") || query.ContainsKey("created"))
        {
            RefreshSensorList();
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

    [RelayCommand]
    private async Task LoadSensors()
    {
        await RefreshSensorList();
    }
}
