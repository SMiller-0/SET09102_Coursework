using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels
{
    public partial class OpsAnomalyManagerViewModel
    {
        public int currentTable = 0;

        private readonly AppDbContext _context;
        public ObservableCollection<System.Object> AllData { get; } = new();
        public OpsAnomalyManagerViewModel(AppDbContext context)
        {
            _context = context;
            LoadData();
        }

        [RelayCommand]
        public void nextPage()
        {
            if (currentTable < 3)
            {
                currentTable++;
                LoadData();
            }
            else
            {
                Shell.Current.DisplayAlert("No more tables found!",
                    $"Please try navigating back through the data for the table you desire.", "OK");
            }
        }

        [RelayCommand]
        public void previousPage()
        {
            if (currentTable > 0)
            {
                currentTable--;
                LoadData();
            }
            else
            {
                Shell.Current.DisplayAlert("No more tables found!",
                    $"Please try navigating forward through the data for the table you desire.", "OK");
            }
        }

        public void LoadData()
        {
            if (currentTable == 0)
            {
                var currentData = _context.AirQData.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                    CheckAirData(info);
                }
            }
            else if (currentTable == 1)
            {
                var currentData = _context.WaterQData.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                    CheckWaterData(info);
                }
            }
            else if (currentTable == 2)
            {
                var currentData = _context.WeatherData.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                    CheckWeatherData(info);
                }
            }
        }

        public void CheckAirData(AirQData? airData)
        {
            if (airData.Nitrogen == null || airData.Sulphur == null || airData.particleMatterSmall == null || airData.particleMatterBig == null)
            {
                FlagColumn = "Anomaly";
            }
            else if (airData.Nitrogen > 50 || airData.Nitrogen < 10)
            {
                FlagColumn = "Anomaly";
            }
            else if (airData.Sulphur > 5 || airData.Sulphur < 0)
            {
                FlagColumn = "Anomaly";
            }
            else if (airData.particleMatterSmall > 20 || airData.particleMatterSmall < 0)
            {
                FlagColumn = "Anomaly";
            }
            else if (airData.particleMatterBig > 20 || airData.particleMatterBig < 0)
            {
                FlagColumn = "Anomaly";
            }
            else
            {
                FlagColumn = "Normal";
            }
        }

        public void CheckWaterData(WaterQData? waterData)
        {
            if (waterData.Nitrate == null || waterData.Nitrite == null || waterData.Phosphate == null || waterData.EscherichiaColi == null)
            {
                FlagColumn = "Anomaly";
            }
            else if (waterData.Nitrate > 30 || waterData.Nitrate < 20)
            {
                FlagColumn = "Anomaly";
            }
            else if (waterData.Nitrite > 2 || waterData.Nitrite < 1)
            {
                FlagColumn = "Anomaly";
            }
            else if (waterData.Phosphate > 0.1 || waterData.Phosphate < 0)
            {
                FlagColumn = "Anomaly";
            }
            else if (waterData.EscherichiaColi > 10 || waterData.EscherichiaColi < 0)
            {
                FlagColumn = "Anomaly";
            }
            else
            {
                FlagColumn = "Normal";
            }
        }

        public void CheckWeatherData(WeatherData? weatherData)
        {
            if (weatherData.AirTemp == null || weatherData.Humidity == null || weatherData.WindDirection == null || weatherData.WindSpeed == null)
            {
                FlagColumn = "Anomaly";
            }
            else if (weatherData.AirTemp > 10 || weatherData.AirTemp < 0)
            {
                FlagColumn = "Anomaly";
            }
            else if (weatherData.Humidity > 100 || weatherData.Humidity < 60)
            {
                FlagColumn = "Anomaly";
            }
            else if (weatherData.WindDirection > 10 || weatherData.WindDirection < 0.5)
            {
                FlagColumn = "Anomaly";
            }
            else if (weatherData.WindSpeed > 500 || weatherData.WindSpeed < 50)
            {
                FlagColumn = "Anomaly";
            }
            else
            {
                FlagColumn = "Normal";
            }
        }
    }
}
