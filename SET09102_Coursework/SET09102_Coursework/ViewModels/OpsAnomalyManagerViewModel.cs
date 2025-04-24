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
        /*! \brief Creates an index to keep track of which table is currently being displayed.*/
        public int currentTable = 0;

        /*! \brief Sets a default value of "Normal" for the anomaly column in the table, used to flag missing or probable incorrect values in the database.*/
        public string FlagColumn { get; set; } = "Normal";

        /*! \brief Creates a model of the application's associated database using Data/AppDbContext class.*/
        private readonly AppDbContext _context;

        /*! \brief This collection is used to display the correct data to users.
         * Due to the use of different data models depending on table, a generic object is used as the type.*/
        public ObservableCollection<System.Object> AllData { get; } = new();

        /*! \brief The constructor for this ViewModel.
        *
        *  Creates a model of the database using the Data/AppDbContext class and calls the LoadData() method to display the first table of data.
        */
        public OpsAnomalyManagerViewModel(AppDbContext context)
        {
            _context = context;
            LoadData();
        }

        /*! \brief A function called when the user clicks the next button on the page.
        *
        *  Updates the table index and calls the LoadData() method to update the information displayed to the next table of data.
        */
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
                /*! \brief An error message for if the table index would go out of bounds.*/
                Shell.Current.DisplayAlert("No more tables found!",
                    $"Please try navigating back through the data for the table you desire.", "OK");
            }
        }

        /*! \brief A function called when the user clicks the back button on the page.
         *
         *  Updates the table index and calls the LoadData() method to update the information displayed to the previous table of data.
         */
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
                /*! \brief An error message for if the table index would go out of bounds.*/
                Shell.Current.DisplayAlert("No more tables found!",
                    $"Please try navigating forward through the data for the table you desire.", "OK");
            }
        }

        /*! \brief A function that loads the correct data from the database for the current table index.
        *
        *  Removes the information currently displayed in the table onscreen and replaces it with the data from the database associated with the current table index.
        */
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
            else
            {
                /*! \brief An error message for if the table index would go out of bounds.*/
                Shell.Current.DisplayAlert("Error! Out of bounds table value.",
                    $"If this issue persists, please contact our customer support team.", "OK");
                currentTable = 0;
            }
        }

        /*! \brief A function used to check the data in the AirQData table for anomalies, including out-of-bounds values and null values.
        *
        *  Updates the anomaly column in the table to "Anomaly" if any of the values do not pass the tests.
        */
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

        /*! \brief A function used to check the data in the WaterQData table for anomalies, including out-of-bounds values and null values.
        *
        *  Updates the anomaly column in the table to "Anomaly" if any of the values do not pass the tests.
        */
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

        /*! \brief A function used to check the data in the WeatherData table for anomalies, including out-of-bounds values and null values.
        *
        *  Updates the anomaly column in the table to "Anomaly" if any of the values do not pass the tests.
        */
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
