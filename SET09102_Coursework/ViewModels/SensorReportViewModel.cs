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

    public ObservableCollection<Sensor> Sensors { get; } = new();
    public ObservableCollection<SensorFilter> FilterOptions { get; } = new();

    [ObservableProperty]
    private SensorFilter selectedFilter;

    partial void OnSelectedFilterChanged(SensorFilter value)
    {
        if (value != null)
        {
            LoadSensorsCommand.Execute(null);
        }
    }

    public SensorReportViewModel(
        ISensorService sensorService,
        ISensorFilterService filterService)
    {
        _sensorService = sensorService;
        _filterService = filterService;
        
        // Load sensors immediately when ViewModel is created
        LoadSensors().ConfigureAwait(false);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        LoadSensors().ConfigureAwait(false);
        query.Clear();
    }

    private async Task InitializeFilterOptionsAsync()
    {
        if (FilterOptions.Count == 0)
        {
            var types = await _sensorService.GetSensorTypesAsync();
            var filters = _filterService.GetTypeFilterOptions(types);
            
            FilterOptions.Clear();
            foreach (var filter in filters)
            {
                FilterOptions.Add(filter);
            }

            SelectedFilter = FilterOptions.First();
        }
    }

    [RelayCommand]
    private async Task LoadSensors()
    {
        await InitializeFilterOptionsAsync();
        var sensorList = await _sensorService.GetSensorsByTypeAsync(null);
        var filteredSensors = _filterService.ApplyTypeFilter(sensorList, SelectedFilter);
        
        Sensors.Clear();
        foreach (var sensor in filteredSensors)
        {
            Sensors.Add(sensor);
        }
    }

    [RelayCommand]
    private async Task GenerateReport(Sensor sensor)
    {
        if (sensor == null) return;
        
        // Here you would implement the report generation logic
        // For now, just show an alert
        await Shell.Current.DisplayAlert("Report Generated", 
            $"Report for sensor {sensor.Name} has been generated.", "OK");
    }
}