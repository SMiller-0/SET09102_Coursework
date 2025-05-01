using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SET09102_Coursework.ViewModels
{
    /*! \brief The code-behind for AnalyticalTestView.xaml to meet the requirements of Issue #11.*/
    public partial class AnalyticalTestsEnviromentalViewModel : ObservableObject
    {
        /*! \brief To prevent an error with trying to load a non-existent set of data when the page is first loaded, the full set of Air Quality data is used to popluate the table.
        * This is probably unnecessary however, as I believe bindings don't throw errors if no data is given or null values could be used, so this variable may be removed later*/
        [ObservableProperty]
        public required AirQData airData;

        /*! \brief Creates a model of the application's associated database using Data/AppDbContext class.*/
        private AppDbContext _context;

        /*! \brief Stores the name of the currently displayed table.*/
        public string CurrentTableName = "Name";

        /*! \brief This collection is used to display the correct data to users.
         * Due to the use of different data models depending on table, a generic object is used as the type.*/
        public ObservableCollection<System.Object> AllData { get; set; } = new();

        /*! \brief The constructor for this ViewModel.
        *
        *  Creates a model of the database using the Data/AppDbContext class and calls the LoadData() method to display the first table of data.
        */
        public AnalyticalTestsEnviromentalViewModel(AppDbContext context)
        {
            _context = context;
            LoadData();
        }

        /*! \brief A list of the data that will be displayed onscreen.
        * Currently empty, since it is only populated after the user has chosen a calculation.*/
        [ObservableProperty]
        private List<string[]> displayDataList = new List<string[]>();
        
        public List<string[]> DisplayDataList
        {
            get => displayDataList;
            set => SetProperty(ref displayDataList, value);
        }

        /*! \brief A function that calulates the highest nitrogen level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrogen value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest nitrogen level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestNitrogenAir()
        {
            CurrentTableName = "Highest Nitrogen (Air Quality)";
            var arrayAirQData = _context.AirQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrogen);
            }
            CalcHighest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the lowest nitrogen level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrogen value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest nitrogen level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestNitrogenAir()
        {
            CurrentTableName = "Lowest Nitrogen (Air Quality)";
            var arrayAirQData = _context.AirQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrogen);
            }
            CalcLowest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the mean nitrogen level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrogen value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  The mean is calculated by adding all the nitrogen levels for each day and dividing by the number of records for that day.
        *  Each record is ordered by date, so the mean nitrogen level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcMeanNitrogenAir()
        {
            CurrentTableName = "Mean Nitrogen (Air Quality)";
            var arrayAirQData = _context.AirQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrogen);
            }
            CalcMean(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the highest sulphur level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the sulphur value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest sulphur level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestSulphurAir()
        {
            CurrentTableName = "Highest Sulphur (Air Quality)";
            var arrayAirQData = _context.AirQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Sulphur);
            }
            CalcHighest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the lowest sulphur level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the sulphur value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest sulphur level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestSulphurAir()
        {
            CurrentTableName = "Lowest Sulphur (Air Quality)";
            var arrayAirQData = _context.AirQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Sulphur);
            }
            CalcLowest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the mean sulphur level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the sulphur value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  The mean is calculated by adding all the sulphur levels for each day and dividing by the number of records for that day.
        *  Each record is ordered by date, so the mean sulphur level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcMeanSulphurAir()
        {
            CurrentTableName = "Mean Sulphur (Air Quality)";
            var arrayAirQData = _context.AirQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Sulphur);
            }
            CalcMean(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the highest nitrite level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrite value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest nitrite level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestNitriteWater()
        {
            CurrentTableName = "Highest Nitrite (Water Quality)";
            var arrayWaterQData = _context.WaterQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrite);
            }
            CalcHighest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the lowest nitrite level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrite value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest nitrite level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestNitriteWater()
        {
            CurrentTableName = "Lowest Nitrite (Water Quality)";
            var arrayWaterQData = _context.WaterQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrite);
            }
            CalcLowest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the mean nitrine level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrine value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  The mean is calculated by adding all the nitrine levels for each day and dividing by the number of records for that day.
        *  Each record is ordered by date, so the mean nitrine level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcMeanNitriteWater()
        {
            CurrentTableName = "Mean Nitrite (Water Quality)";
            var arrayWaterQData = _context.WaterQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrite);
            }
            CalcMean(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the highest nitrate level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrate value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest nitrate level for each day is displayed, allowing for a quick overview of the data.
        */

        [RelayCommand]
        public void CalcHighestNitrateWater()
        {
            CurrentTableName = "Highest Nitrate (Water Quality)";
            var arrayWaterQData = _context.WaterQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrate);
            }
            CalcHighest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the lowest nitrate level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrate value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest nitrate level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestNitrateWater()
        {
            CurrentTableName = "Lowest Nitrate (Water Quality)";
            var arrayWaterQData = _context.WaterQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Nitrate);
            }
            CalcLowest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the mean nitrate level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrate value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  The mean is calculated by adding all the nitrate levels for each day and dividing by the number of records for that day.
        *  Each record is ordered by date, so the mean nitrate level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcMeanNitrateWater()
        {
            CurrentTableName = "Mean Nitrate (Water Quality)";
            var arrayWaterQData = _context.WaterQData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WaterQData item in arrayWaterQData)
            {
              DateTime itemDate = item.Date;
              dateList.Add(itemDate);
              idList.Add(item.Id);
              valueList.Add(item.Nitrate);
            }
                CalcMean(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the highest air temperature in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the air temperature and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest air temperature for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestAirTempWeather()
        {
            CurrentTableName = "Highest Air Temperature (Weather Data)";
            var arrayWeatherData = _context.WeatherData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.AirTemp);
            }
            CalcHighest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the lowest air temperature in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the air temperature and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest air temperature for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestAirTempWeather()
        {
            CurrentTableName = "Lowest Air Temperature (Weather Data)";
            var arrayWeatherData = _context.WeatherData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.AirTemp);
            }
            CalcLowest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the mean air temperature level in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the air temperature and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  The mean is calculated by adding all the air temperatures for each day and dividing by the number of records for that day.
        *  Each record is ordered by date, so the mean air temperature for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcMeanAirTempWeather()
        {
            CurrentTableName = "Mean Air Temperature (Weather Data)";
            var arrayWeatherData = _context.WeatherData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.AirTemp);
            }
            CalcMean(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the highest humidity level in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the humidity value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest humidity level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestHumidityWeather()
        {
            CurrentTableName = "Highest Humidity (Weather Data)";
            var arrayWeatherData = _context.WeatherData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WeatherData item in arrayWeatherData)
        {
             DateTime itemDate = item.Date;
            dateList.Add(itemDate);
            idList.Add(item.Id);
            valueList.Add(item.Humidity);
        }
        CalcHighest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the lowest humidity level in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the humidity value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest humidity level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestHumidityWeather()
        {
            CurrentTableName = "Lowest Humidity (Weather Data)";
            var arrayWeatherData = _context.WeatherData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Humidity);
            }
            CalcLowest(idList, valueList, dateList);
        }

        /*! \brief A function that calulates the mean humidity level in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the humidity value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  The mean is calculated by adding all the humidity levels for each day and dividing by the number of records for that day.
        *  Each record is ordered by date, so the mean humidity level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcMeanHumidityWeather()
        {
            CurrentTableName = "Mean Humidity (Weather Data)";
            var arrayWeatherData = _context.WeatherData.ToArray();
            var idList = new List<int>();
            var valueList = new List<int>();
            var dateList = new List<DateTime>();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                dateList.Add(itemDate);
                idList.Add(item.Id);
                valueList.Add(item.Humidity);
            }
            CalcMean(idList, valueList, dateList);
        }

        /*! \brief A function that navigates the AllUsersPage; the current homepage for the application.
        *
        *  This function is a little redundant as users can navigate directly to the AllUsersPage using the navigation banner at the bottom of the screen, but it is included for the sake of completeness.
        */
        [RelayCommand]
        public void ReturnToHome()
        {
            if (Shell.Current != null)
            {
                try
                {
                    Shell.Current.GoToAsync("//AllUsersPage");
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    Console.WriteLine($"Navigation error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Shell.Current is not initialized.");
            }
        }

        /*! \brief A function that loads the correct data from the database for the current table index.
        *
        *  Removes the information currently displayed in the table onscreen and replaces it with the data from the database associated with the current table index.
        */
        public void LoadData()
        {
            var currentData = _context.AirQData.ToList();
            AllData.Clear();
            if (AllData.Count == 0)
            {
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else
            {
                Console.WriteLine("Failed to clear AllData. Aborting data load to prevent duplicates.");
            }
        }

        private void CalcHighest(List<int> idList, List<int> valueList, List<DateTime> dateList)
        {
            var outputData = new List<int[]>();
            for (int i = 0; i < idList.Count; i++)
            {
                DateTime itemDate = dateList[i];
                int highestId = idList[i];
                int highestValue = valueList[i];
                for(int j = 0; j < idList.Count; j++)
                {
                    if (dateList[j] == dateList[i])
                    {
                        if (valueList[j] > highestValue)
                        {
                            highestId = idList[j];
                            highestValue = valueList[j];
                        }
                    }
                    else
                    {
                        if (!outputData.Any(data => data[0] == highestId))
                        {
                            outputData.Add(new int[] { highestId, highestValue });
                        }
                        break;
                    }
                }
                DisplayDataList = outputData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        private void CalcLowest(List<int> idList, List<int> valueList, List<DateTime> dateList)
        {
            var outputData = new List<int[]>();
            for (int i = 0; i < idList.Count; i++)
            {
                DateTime itemDate = dateList[i];
                int lowestId = idList[i];
                int lowestValue = valueList[i];
                for (int j = 0; j < idList.Count; j++)
                {
                    if (dateList[j] == dateList[i])
                    {
                        if (valueList[j] < lowestValue)
                        {
                            lowestId = idList[j];
                            lowestValue = valueList[j];
                        }
                    }
                    else
                    {
                        if (!outputData.Any(data => data[0] == lowestId))
                        {
                            outputData.Add(new int[] { lowestId, lowestValue });
                        }
                        break;
                    }
                }
                DisplayDataList = outputData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        public void CalcMean(List<int> idList, List<int> valueList, List<DateTime> dateList)
        {
            var outputData = new List<int[]>();
            List<int> arrayPerDay = new List<int>();
            int totalValue = 0;
            for (int i = 0; i < idList.Count; i++)
            {
                DateTime itemDate = dateList[i];
                int meanId = idList[i];
                int meanValue = valueList[i];
                for (int j = 0; j < idList.Count; j++)
                {
                    if (dateList[j] == itemDate)
                    {
                        totalValue += valueList[j];
                        arrayPerDay.Add(valueList[j]);
                    }
                    else
                    {
                        int meanTotalValue = totalValue / arrayPerDay.Count;
                        outputData.Add(new int[] { meanId, meanTotalValue });
                        totalValue = 0;
                        arrayPerDay.Clear();
                        arrayPerDay.Add(valueList[j]);
                        break;
                    }
                }
                DisplayDataList = outputData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }
    }
}
