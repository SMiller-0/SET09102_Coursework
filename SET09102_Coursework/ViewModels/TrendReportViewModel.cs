using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SET09102_Coursework.ViewModels;

public partial class TrendReportViewModel : ObservableObject, IQueryAttributable
{
    private readonly IMeasurementService _measurementService;

    [ObservableProperty]
    private Sensor sensor;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string reportTitle;

    public ObservableCollection<MeasurementStatistic> Statistics { get; } = new();

    public TrendReportViewModel(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("sensor", out var sensorObj) && sensorObj is Sensor sensorValue)
        {
            Sensor = sensorValue;
            ReportTitle = $"Statistical Analysis for {Sensor.Name}";
            LoadStatisticsCommand.Execute(null);
        }
    }

    [RelayCommand]
    private async Task LoadStatistics()
    {
        if (Sensor == null) return;

        try
        {
            IsLoading = true;
            Statistics.Clear();

            var sensorType = Sensor.SensorType.Name.ToLower();
            var statistics = await _measurementService.GetSensorStatisticsAsync(Sensor.Id, sensorType);

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

