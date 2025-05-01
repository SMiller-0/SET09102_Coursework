using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;

namespace SET09102_Coursework.ViewModels;

public partial class SensorStatusViewModel : ObservableObject, IDisposable
{
    private readonly ISensorRefreshService _refreshService;
    private readonly ISensorFilterService _filterService;
    private const int REFRESH_INTERVAL_SECONDS = 5;

    public ObservableCollection<Sensor> Sensors { get; } = new();
    public ObservableCollection<SensorStatusFilter> FilterOptions { get; } = new();

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private SensorStatusFilter selectedFilter;

    private IEnumerable<Sensor> _allSensors = new List<Sensor>();

    public SensorStatusViewModel(
        ISensorRefreshService refreshService,
        ISensorFilterService filterService)
    {
        _refreshService = refreshService;
        _filterService = filterService;
        _refreshService.SensorsRefreshed += OnSensorsRefreshed;
        InitializeFilters();
        InitializeAsync();
    }

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

    partial void OnSelectedFilterChanged(SensorStatusFilter value)
    {
        if (value != null)
        {
            ApplyFilter();
        }
    }

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

    private async void InitializeAsync()
    {
        await LoadSensors();
        _refreshService.StartAutoRefresh(REFRESH_INTERVAL_SECONDS);
    }

    private void OnSensorsRefreshed(object? sender, IEnumerable<Sensor> sensors)
    {
        _allSensors = sensors;
        ApplyFilter();
    }

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

    public void Dispose()
    {
        _refreshService.Dispose();
        GC.SuppressFinalize(this);
    }
}



