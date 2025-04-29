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
            get => DisplayDataList;
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
            var outputAirQData = _context.AirQData
                .GroupBy(data => data.Date)
                .Select(group => new
                {
                    HighestNitrogenId = group.OrderByDescending(item => item.Nitrogen).First().Id,
                    HighestNitrogen = group.Max(item => item.Nitrogen)
                })
                .Select(result => new int[] { result.HighestNitrogenId, result.HighestNitrogen })
                .ToList();

            // Display outputAirQData on-screen:
        }

        /*! \brief A function that calulates the lowest nitrogen level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrogen value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest nitrogen level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestNitrogenAir()
        {
            int lowestNitrogenId = 0;
            int lowestNitrogen = 0;
            var outputAirQData = new List<int[]>();
            var arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                lowestNitrogenId = item.Id;
                lowestNitrogen = item.Nitrogen;

                foreach (var item2 in arrayAirQData.Where(item2 => item2.Date == itemDate))
                {
                    if (item2.Nitrogen < lowestNitrogen)
                    {
                        lowestNitrogenId = item2.Id;
                        lowestNitrogen = item2.Nitrogen;
                    }
                }

                if (!outputAirQData.Any(data => data[0] == lowestNitrogenId))
                {
                    outputAirQData.Add(new[] { lowestNitrogenId, lowestNitrogen });
                }
            }
            DisplayDataList = outputAirQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
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
            int meanNitrogenId = 0;
            int meanNitrogen = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            List<int> arrayNitrogenPerDay = new List<int>();
            int totalNitrogen = 0;
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                meanNitrogenId = item.Id;
                meanNitrogen = item.Nitrogen;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        totalNitrogen += item2.Nitrogen;
                        arrayNitrogenPerDay.Add(item2.Nitrogen);
                    }
                    else
                    {
                        meanNitrogen = totalNitrogen / arrayNitrogenPerDay.Count;
                        outputAirQData.Add(new int[] { meanNitrogenId, meanNitrogen });
                        totalNitrogen = 0;
                        arrayNitrogenPerDay.Clear();
                        arrayNitrogenPerDay.Add(item2.Sulphur);
                        break;
                    }
                }
                DisplayDataList = outputAirQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the highest sulphur level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the sulphur value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest sulphur level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestSulphurAir()
        {
            int highestSulphurId = 0;
            int highestSulphur = 0;
            var outputAirQData = new List<int[]>();
            var arrayAirQData = _context.AirQData.ToArray();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                highestSulphurId = item.Id;
                highestSulphur = item.Sulphur;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Sulphur > highestSulphur)
                        {
                            highestSulphurId = item2.Id;
                            highestSulphur = item2.Sulphur;
                        }
                    }
                    else
                    {
                        if (!outputAirQData.Any(data => data[0] == highestSulphurId))
                        {
                            outputAirQData.Add(new int[] { highestSulphurId, highestSulphur });
                        }
                        break;
                    }
                }
                DisplayDataList = outputAirQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the lowest sulphur level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the sulphur value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest sulphur level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestSulphurAir()
        {
            int lowestSulphurId = 0;
            int lowestSulphur = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            AirQData[] arrayAirQData = _context.AirQData.ToArray();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                lowestSulphurId = item.Id;
                lowestSulphur = item.Sulphur;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Sulphur < lowestSulphur)
                        {
                            lowestSulphurId = item2.Id;
                            lowestSulphur = item2.Sulphur;
                        }
                    }
                    outputAirQData.Add(new int[] { lowestSulphurId, lowestSulphur });
                    break;
                }
                DisplayDataList = outputAirQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
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
            int meanSulphurId = 0;
            int meanSulphur = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            List<int> arraySulphurPerDay = new List<int>();
            int totalSulphur = 0;
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                meanSulphurId = item.Id;
                meanSulphur = item.Sulphur;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        totalSulphur += item2.Sulphur;
                        arraySulphurPerDay.Add(item2.Sulphur);
                    }
                    else
                    {
                        meanSulphur = totalSulphur / arraySulphurPerDay.Count;
                        outputAirQData.Add(new int[] { meanSulphurId, meanSulphur });
                        totalSulphur = 0;
                        arraySulphurPerDay.Clear();
                        arraySulphurPerDay.Add(item2.Sulphur);
                        break;
                    }
                }
                DisplayDataList = outputAirQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();

            }
        }

        /*! \brief A function that calulates the highest nitrite level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrite value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest nitrite level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestNitriteWater()
        {
            int highestNitriteId = 0;
            int highestNitrite = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            WaterQData[] arrayWaterQData = _context.WaterQData.ToArray();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                highestNitriteId = item.Id;
                highestNitrite = item.Nitrite;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Nitrite > highestNitrite)
                        {
                            highestNitriteId = item2.Id;
                            highestNitrite = item2.Nitrite;
                        }
                        outputWaterQData.Add(new int[] { highestNitriteId, highestNitrite });
                        break;
                    }
                }
                DisplayDataList = outputWaterQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the lowest nitrite level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrite value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest nitrite level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestNitriteWater()
        {
            int lowestNitriteId = 0;
            int lowestNitrite = 0;
            WaterQData[] arrayWaterQData = _context.WaterQData.ToArray();
            List<int[]> outputWaterQData = new List<int[]>();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                lowestNitriteId = item.Id;
                lowestNitrite = item.Nitrite;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Nitrite < lowestNitrite)
                        {
                            lowestNitriteId = item2.Id;
                            lowestNitrite = item2.Nitrite;
                        }
                        outputWaterQData.Add(new int[] { lowestNitriteId, lowestNitrite });
                        break;
                    }
                }
                DisplayDataList = outputWaterQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
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
                int meanNitriteId = 0;
                int meanNitrite = 0;
                List<int[]> outputWaterQData = new List<int[]>(); ;
                Array arrayWaterQData = _context.WaterQData.ToArray();
                List<int> arrayNitritePerDay = new List<int>();
                int totalNitrite = 0;
                foreach (WaterQData item in arrayWaterQData)
                {
                    DateTime itemDate = item.Date;
                    meanNitriteId = item.Id;
                    meanNitrite = item.Nitrite;
                    foreach (WaterQData item2 in arrayWaterQData)
                    {
                        if (item2.Date == itemDate)
                        {
                            totalNitrite += item2.Nitrite;
                            arrayNitritePerDay.Add(item2.Nitrite);
                        }
                        else
                        {
                            meanNitrite = totalNitrite / arrayNitritePerDay.Count;
                            outputWaterQData.Add(new int[] { meanNitriteId, meanNitrite });
                            totalNitrite = 0;
                            arrayNitritePerDay.Clear();
                            arrayNitritePerDay.Add(item2.Nitrite);
                            break;
                        }
                    }
                    DisplayDataList = outputWaterQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
                }
        }

        /*! \brief A function that calulates the highest nitrate level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrate value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest nitrate level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestNitrateWater()
        {
            int highestNitrateId = 0;
            int highestNitrate = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                highestNitrateId = item.Id;
                highestNitrate = item.Nitrate;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Nitrate > highestNitrate)
                        {
                            highestNitrateId = item2.Id;
                            highestNitrate = item2.Nitrate;
                        }
                        outputWaterQData.Add(new int[] { highestNitrateId, highestNitrate });
                        break;
                    }
                }
                DisplayDataList = outputWaterQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the lowest nitrate level in the water quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrate value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest nitrate level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestNitrateWater()
        {
            int lowestNitrateId = 0;
            int lowestNitrate = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                lowestNitrateId = item.Id;
                lowestNitrate = item.Nitrate;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Nitrate < lowestNitrate)
                        {
                            lowestNitrateId = item2.Id;
                            lowestNitrate = item2.Nitrate;
                        }
                        outputWaterQData.Add(new int[] { lowestNitrateId, lowestNitrate });
                        break;
                    }
                }
                DisplayDataList = outputWaterQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
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
            int meanNitrateId = 0;
            int meanNitrate = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            List<int> arrayNitratePerDay = new List<int>();
            int totalNitrate = 0;
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                meanNitrateId = item.Id;
                meanNitrate = item.Nitrate;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        totalNitrate += item2.Nitrate;
                        arrayNitratePerDay.Add(item2.Nitrate);
                    }
                    else
                    {
                        meanNitrate = totalNitrate / arrayNitratePerDay.Count;
                        outputWaterQData.Add(new int[] { meanNitrateId, meanNitrate });
                        totalNitrate = 0;
                        arrayNitratePerDay.Clear();
                        arrayNitratePerDay.Add(item2.Nitrate);
                        break;
                    }
                }
                DisplayDataList = outputWaterQData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the highest air temperature in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the air temperature and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest air temperature for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestAirTempWeather()
        {
            int highestAirTempId = 0;
            int highestAirTemp = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            WeatherData[] arrayWeatherData = _context.WeatherData.ToArray();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                highestAirTempId = item.Id;
                highestAirTemp = item.AirTemp;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.AirTemp > highestAirTemp)
                        {
                            highestAirTempId = item2.Id;
                            highestAirTemp = item2.AirTemp;
                        }
                        outputWeatherData.Add(new int[] { highestAirTempId, highestAirTemp });
                        break;
                    }
                }
                DisplayDataList = outputWeatherData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the lowest air temperature in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the air temperature and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest air temperature for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestAirTempWeather()
        {
            int lowestAirTempId = 0;
            int lowestAirTemp = 0;
            WeatherData[] arrayWeatherData = _context.WeatherData.ToArray();
            List<int[]> outputWeatherData = new List<int[]>();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                lowestAirTempId = item.Id;
                lowestAirTemp = item.AirTemp;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.AirTemp < lowestAirTemp)
                        {
                            lowestAirTempId = item2.Id;
                            lowestAirTemp = item2.AirTemp;
                        }
                        outputWeatherData.Add(new int[] { lowestAirTempId, lowestAirTemp });
                        break;
                    }
                }
                DisplayDataList = outputWeatherData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
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
            int meanAirTempId = 0;
            int meanAirTemp = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            List<int> arrayAirTempPerDay = new List<int>();
            int totalAirTemp = 0;
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                meanAirTempId = item.Id;
                meanAirTemp = item.AirTemp;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        totalAirTemp += item2.AirTemp;
                        arrayAirTempPerDay.Add(item2.AirTemp);
                    }
                    else
                    {
                        meanAirTemp = totalAirTemp / arrayAirTempPerDay.Count;
                        outputWeatherData.Add(new int[] { meanAirTempId, meanAirTemp });
                        totalAirTemp = 0;
                        arrayAirTempPerDay.Clear();
                        arrayAirTempPerDay.Add(item2.AirTemp);
                        break;
                    }
                }
                DisplayDataList = outputWeatherData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the highest humidity level in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the humidity value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest humidity level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestHumidityWeather()
        {
            int highestHumidityId = 0;
            int highestHumidity = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                highestHumidityId = item.Id;
                highestHumidity = item.Humidity;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Humidity > highestHumidity)
                        {
                            highestHumidityId = item2.Id;
                            highestHumidity = item2.Humidity;
                        }
                        outputWeatherData.Add(new int[] { highestHumidityId, highestHumidity });
                        break;
                    }
                }
                DisplayDataList = outputWeatherData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
        }

        /*! \brief A function that calulates the lowest humidity level in the weather data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the humidity value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the lowest humidity level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcLowestHumidityWeather()
        {
            int lowestHumidityId = 0;
            int lowestHumidity = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                lowestHumidityId = item.Id;
                lowestHumidity = item.Humidity;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Humidity < lowestHumidity)
                        {
                            lowestHumidityId = item2.Id;
                            lowestHumidity = item2.Humidity;
                        }
                        outputWeatherData.Add(new int[] { lowestHumidityId, lowestHumidity });
                        break;
                    }
                }
                DisplayDataList = outputWeatherData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
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
            int meanHumidityId = 0;
            int meanHumidity = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            List<int> arrayHumidityPerDay = new List<int>();
            int totalHumidity = 0;
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                meanHumidityId = item.Id;
                meanHumidity = item.Humidity;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        totalHumidity += item2.Humidity;
                        arrayHumidityPerDay.Add(item2.Humidity);
                    }
                    else
                    {
                        meanHumidity = totalHumidity / arrayHumidityPerDay.Count;
                        outputWeatherData.Add(new int[] { meanHumidityId, meanHumidity });
                        totalHumidity = 0;
                        arrayHumidityPerDay.Clear();
                        arrayHumidityPerDay.Add(item2.Humidity);
                        break;
                    }
                }
                DisplayDataList = outputWeatherData.Select(data => new string[] { data[0].ToString(), data[1].ToString() }).ToList();
            }
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
    }
}
