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

        /*! \brief The id of a record that meets the calculation criteria.
        * Currently 0 as this is the integer equivalent of null.*/
        [ObservableProperty]
        private int columnId = 0;

        /*! \brief The value, e.g. nitrogen level, of a record that meets the calculation criteria.
        * Currently 0 as this is the integer equivalent of null.*/
        [ObservableProperty]
        private int columnData = 0;

        /*! \brief A function that calulates the highest nitrogen level in the air quality data and then recreates a table of the data, displaying it onscreen.
        *
        *  Only the nitrogen value and record id are displayed for the sake of simplicity, but this could be expanded to include other values if needed.
        *  Each record is ordered by date, so the highest nitrogen level for each day is displayed, allowing for a quick overview of the data.
        */
        [RelayCommand]
        public void CalcHighestNitrogenAir()
        {
            int highestNitrogenId = 0;
            int highestNitrogen = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                highestNitrogenId = item.Id;
                highestNitrogen = item.Nitrogen;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Nitrogen > highestNitrogen)
                        {
                            highestNitrogenId = item2.Id;
                            highestNitrogen = item2.Nitrogen;
                        }
                        highestNitrogenId = item2.Id;
                        highestNitrogen = item2.Nitrogen;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { highestNitrogenId, highestNitrogen });
                        break;
                    }
                }
                //Display output data on-screen:
            }
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
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                lowestNitrogenId = item.Id;
                lowestNitrogen = item.Nitrogen;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        if (item2.Nitrogen < lowestNitrogen)
                        {
                            lowestNitrogenId = item2.Id;
                            lowestNitrogen = item2.Nitrogen;
                        }
                        lowestNitrogenId = item2.Id;
                        lowestNitrogen = item2.Nitrogen;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { lowestNitrogenId, lowestNitrogen });
                        break;
                    }
                }
                //Display output data on-screen:
            }
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
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                meanNitrogenId = item.Id;
                meanNitrogen = item.Nitrogen;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        meanNitrogen+= item2.Nitrogen;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { meanNitrogenId, meanNitrogen });
                        break;
                    }
                }
                //Display output data on-screen:
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
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
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
                        highestSulphurId = item2.Id;
                        highestSulphur = item2.Sulphur;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { highestSulphurId, highestSulphur });
                        break;
                    }
                }
                //Display output data on-screen:
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
            Array arrayAirQData = _context.AirQData.ToArray();
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
                        lowestSulphurId = item2.Id;
                        lowestSulphur = item2.Sulphur;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { lowestSulphurId, lowestSulphur });
                        break;
                    }
                }
                //Display output data on-screen:
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
            foreach (AirQData item in arrayAirQData)
            {
                DateTime itemDate = item.Date;
                meanSulphurId = item.Id;
                meanSulphur = item.Sulphur;
                foreach (AirQData item2 in arrayAirQData)
                {
                    if (item2.Date == itemDate)
                    {
                        meanSulphur += item2.Sulphur;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { meanSulphurId, meanSulphur });
                        break;
                    }
                }
                //Display output data on-screen:
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
            Array arrayWaterQData = _context.WaterQData.ToArray();
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
                        highestNitriteId = item2.Id;
                        highestNitrite = item2.Nitrite;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { highestNitriteId, highestNitrite });
                        break;
                    }
                }
                //Display output data on-screen:
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
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
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
                        lowestNitriteId = item2.Id;
                        lowestNitrite = item2.Nitrite;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { lowestNitriteId, lowestNitrite });
                        break;
                    }
                }
                //Display output data on-screen:
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
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                meanNitriteId = item.Id;
                meanNitrite = item.Nitrite;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        meanNitrite += item2.Nitrite;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { meanNitriteId, meanNitrite });
                        break;
                    }
                }
                //Display output data on-screen:
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
                        highestNitrateId = item2.Id;
                        highestNitrate = item2.Nitrate;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { highestNitrateId, highestNitrate });
                        break;
                    }
                }
                //Display output data on-screen:
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
                        lowestNitrateId = item2.Id;
                        lowestNitrate = item2.Nitrate;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { lowestNitrateId, lowestNitrate });
                        break;
                    }
                }
                //Display output data on-screen:
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
            foreach (WaterQData item in arrayWaterQData)
            {
                DateTime itemDate = item.Date;
                meanNitrateId = item.Id;
                meanNitrate = item.Nitrate;
                foreach (WaterQData item2 in arrayWaterQData)
                {
                    if (item2.Date == itemDate)
                    {
                        meanNitrate += item2.Nitrate;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { meanNitrateId, meanNitrate });
                        break;
                    }
                }
                //Display output data on-screen:
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
            Array arrayWeatherData = _context.WeatherData.ToArray();
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
                        highestAirTempId = item2.Id;
                        highestAirTemp = item2.AirTemp;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { highestAirTempId, highestAirTemp });
                        break;
                    }
                }
                //Display output data on-screen:
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
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
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
                        lowestAirTempId = item2.Id;
                        lowestAirTemp = item2.AirTemp;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { lowestAirTempId, lowestAirTemp });
                        break;
                    }
                }
                //Display output data on-screen:
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
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                meanAirTempId = item.Id;
                meanAirTemp = item.AirTemp;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        meanAirTemp += item2.AirTemp;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { meanAirTempId, meanAirTemp });
                        break;
                    }
                }
                //Display output data on-screen:
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
                        highestHumidityId = item2.Id;
                        highestHumidity = item2.Humidity;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { highestHumidityId, highestHumidity });
                        break;
                    }
                }
                //Display output data on-screen:
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
                        lowestHumidityId = item2.Id;
                        lowestHumidity = item2.Humidity;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { lowestHumidityId, lowestHumidity });
                        break;
                    }
                }
                //Display output data on-screen:
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
            foreach (WeatherData item in arrayWeatherData)
            {
                DateTime itemDate = item.Date;
                meanHumidityId = item.Id;
                meanHumidity = item.Humidity;
                foreach (WeatherData item2 in arrayWeatherData)
                {
                    if (item2.Date == itemDate)
                    {
                        meanHumidity += item2.Humidity;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { meanHumidityId, meanHumidity });
                        break;
                    }
                }
                //Display output data on-screen:
            }
        }

        /*! \brief A function that navigates the AllUsersPage; the current homepage for the application.
        *
        *  This function is a little redundant as users can navigate directly to the AllUsersPage using the navigation banner at the bottom of the screen, but it is included for the sake of completeness.
        */
        [RelayCommand]
        public void ReturnToHome()
        {
            Shell.Current.GoToAsync("//AllUsersPage");
        }

        /*! \brief A function that loads the correct data from the database for the current table index.
        *
        *  Removes the information currently displayed in the table onscreen and replaces it with the data from the database associated with the current table index.
        */
        public void LoadData()
        {
            var currentData = _context.AirQData.ToList();
            AllData.Clear();
            foreach (var info in currentData)
            {
                AllData.Add(info);
            }
        }
    }
}
