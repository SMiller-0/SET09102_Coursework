using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SET09102_Coursework.ViewModels;

/// <summary>
/// ViewModel for displaying trend reports and statistical analysis for sensor data.
/// </summary>
public partial class TrendReportViewModel : ObservableObject, IQueryAttributable
{
    private readonly IReportService _reportService;

    /// <summary>
    /// The sensor for which the trend report is being generated.
    /// </summary>
    [ObservableProperty]
    private Sensor sensor;

    /// <summary>
    /// Indicates whether data is currently being loaded.
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// The title of the report.
    /// </summary>
    [ObservableProperty]
    private string reportTitle;

    /// <summary>
    /// Collection of measurement statistics to display in the report.
    /// </summary>
    public ObservableCollection<MeasurementStatistic> Statistics { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TrendReportViewModel"/> class.
    /// </summary>
    /// <param name="reportService">The report service used to generate trend reports.</param>
    public TrendReportViewModel(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Applies query attributes when navigating to this page.
    /// </summary>
    /// <param name="query">The query parameters passed during navigation.</param>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("sensor", out var sensorObj) && sensorObj is Sensor sensorValue)
        {
            Sensor = sensorValue;
            ReportTitle = $"Statistical Analysis for {Sensor.Name}";
            LoadStatisticsCommand.Execute(null);
        }
    }

    /// <summary>
    /// Loads statistics for the current sensor.
    /// </summary>
    [RelayCommand]
    private async Task LoadStatistics()
    {
        if (Sensor == null) return;

        try
        {
            IsLoading = true;
            Statistics.Clear();

            var statistics = await _reportService.GenerateTrendReportAsync(Sensor);

            foreach (var stat in statistics)
            {
                Statistics.Add(stat);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", 
                $"Failed to load statistics: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task GoBack()
    {
        try
        {
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Navigation error: {ex.Message}");
            
            // Try alternative navigation if the standard approach fails
            await Shell.Current.GoToAsync("///SensorDashboardPage/SensorReportPage");
        }
    }
}

