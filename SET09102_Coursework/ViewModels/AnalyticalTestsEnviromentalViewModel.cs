using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;

namespace SET09102_Coursework.ViewModels
{
    public partial class AnalyticalTestsEnviromentalViewModel : ObservableObject
    {
        [ObservableProperty]
        public AirQData airData;

        private AppDbContext _context;

        public ObservableCollection<AnalyticalTestsEnviromentalViewModel> tableData = new AnalyticalTestsEnviromentalViewModel
        {
            ColumnId = 0,
            ColumnData = 0
        };

        [RelayCommand]
        public void CalcHighestNitrogenAir()
        {
            int highestNitrogenId = 0;
            int highestNitrogen = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                Date itemDate = item.Date.Date;
                highestNitrogenId = item.Id;
                highestNitrogen = item.Nitrogen;
                foreach (var item2 in arrayAirQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputAirQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcLowestNitrogenAir()
        {
            int lowestNitrogenId = 0;
            int lowestNitrogen = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                Date itemDate = item.Date.Date;
                lowestNitrogenId = item.Id;
                lowestNitrogen = item.Nitrogen;
                foreach (var item2 in arrayAirQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputAirQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcMeanNitrogenAir()
        {
            int meanNitrogenId = 0;
            int meanNitrogen = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                Date itemDate = item.Date.Date;
                meanNitrogenId = item.Id;
                meanNitrogen = item.Nitrogen;
                foreach (var item2 in arrayAirQData)
                {
                    if (item2.Date.Date == itemDate)
                    {
                        meanNitrogen+= item2.Nitrogen;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { meanNitrogenId, meanNitrogen });
                        break;
                    }
                }
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputAirQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcHighestSulphurAir()
        {
            int highestSulphurId = 0;
            int highestSulphur = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                Date itemDate = item.Date.Date;
                highestSulphurId = item.Id;
                highestSulphur = item.Sulphur;
                foreach (var item2 in arrayAirQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputAirQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcLowestSulphurAir()
        {
            int lowestSulphurId = 0;
            int lowestSulphur = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                Date itemDate = item.Date.Date;
                lowestSulphurId = item.Id;
                lowestSulphur = item.Sulphur;
                foreach (var item2 in arrayAirQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputAirQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcMeanSulphurAir()
        {
            int meanSulphurId = 0;
            int meanSulphur = 0;
            List<int[]> outputAirQData = new List<int[]>(); ;
            Array arrayAirQData = _context.AirQData.ToArray();
            foreach (var item in arrayAirQData)
            {
                Date itemDate = item.Date.Date;
                meanSulphurId = item.Id;
                meanSulphur = item.Sulphur;
                foreach (var item2 in arrayAirQData)
                {
                    if (item2.Date.Date == itemDate)
                    {
                        meanSulphur += item2.Sulphur;
                    }
                    else
                    {
                        outputAirQData.Add(new int[] { meanSulphurId, meanSulphur });
                        break;
                    }
                }
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputAirQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcHighestNitriteWater()
        {
            int highestNitriteId = 0;
            int highestNitrite = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (var item in arrayWaterQData)
            {
                Date itemDate = item.Date.Date;
                highestNitriteId = item.Id;
                highestNitrite = item.Nitrite;
                foreach (var item2 in arrayWaterQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWaterQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcLowestNitriteWater()
        {
            int lowestNitriteId = 0;
            int lowestNitrite = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (var item in arrayWaterQData)
            {
                Date itemDate = item.Date.Date;
                lowestNitriteId = item.Id;
                lowestNitrite = item.Nitrite;
                foreach (var item2 in arrayWaterQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWaterQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcMeanNitriteWater()
        {
            int meanNitriteId = 0;
            int meanNitrite = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (var item in arrayWaterQData)
            {
                Date itemDate = item.Date.Date;
                meanNitriteId = item.Id;
                meanNitrite = item.Nitrite;
                foreach (var item2 in arrayWaterQData)
                {
                    if (item2.Date.Date == itemDate)
                    {
                        meanNitrite += item2.Nitrite;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { meanNitriteId, meanNitrite });
                        break;
                    }
                }
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWaterQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcHighestNitrateWater()
        {
            int highestNitrateId = 0;
            int highestNitrate = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (var item in arrayWaterQData)
            {
                Date itemDate = item.Date.Date;
                highestNitrateId = item.Id;
                highestNitrate = item.Nitrate;
                foreach (var item2 in arrayWaterQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWaterQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcLowestNitrateWater()
        {
            int lowestNitrateId = 0;
            int lowestNitrate = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (var item in arrayWaterQData)
            {
                Date itemDate = item.Date.Date;
                lowestNitrateId = item.Id;
                lowestNitrate = item.Nitrate;
                foreach (var item2 in arrayWaterQData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWaterQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcMeanNitrateWater()
        {
            int meanNitrateId = 0;
            int meanNitrate = 0;
            List<int[]> outputWaterQData = new List<int[]>(); ;
            Array arrayWaterQData = _context.WaterQData.ToArray();
            foreach (var item in arrayWaterQData)
            {
                Date itemDate = item.Date.Date;
                meanNitrateId = item.Id;
                meanNitrate = item.Nitrate;
                foreach (var item2 in arrayWaterQData)
                {
                    if (item2.Date.Date == itemDate)
                    {
                        meanNitrate += item2.Nitrate;
                    }
                    else
                    {
                        outputWaterQData.Add(new int[] { meanNitrateId, meanNitrate });
                        break;
                    }
                }
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWaterQData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcHighestAirTempWeather()
        {
            int highestAirTempId = 0;
            int highestAirTemp = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (var item in arrayWeatherData)
            {
                Date itemDate = item.Date.Date;
                highestAirTempId = item.Id;
                highestAirTemp = item.AirTemp;
                foreach (var item2 in arrayWeatherData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWeatherData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcLowestAirTempWeather()
        {
            int lowestAirTempId = 0;
            int lowestAirTemp = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (var item in arrayWeatherData)
            {
                Date itemDate = item.Date.Date;
                lowestAirTempId = item.Id;
                lowestAirTemp = item.AirTemp;
                foreach (var item2 in arrayWeatherData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWeatherData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcMeanAirTempWeather()
        {
            int meanAirTempId = 0;
            int meanAirTemp = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (var item in arrayWeatherData)
            {
                Date itemDate = item.Date.Date;
                meanAirTempId = item.Id;
                meanAirTemp = item.AirTemp;
                foreach (var item2 in arrayWeatherData)
                {
                    if (item2.Date.Date == itemDate)
                    {
                        meanAirTemp += item2.AirTemp;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { meanAirTempId, meanAirTemp });
                        break;
                    }
                }
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWeatherData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcHighestHumidityWeather()
        {
            int highestHumidityId = 0;
            int highestHumidity = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (var item in arrayWeatherData)
            {
                Date itemDate = item.Date.Date;
                highestHumidityId = item.Id;
                highestHumidity = item.Humidity;
                foreach (var item2 in arrayWeatherData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWeatherData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcLowestHumidityWeather()
        {
            int lowestHumidityId = 0;
            int lowestHumidity = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (var item in arrayWeatherData)
            {
                Date itemDate = item.Date.Date;
                lowestHumidityId = item.Id;
                lowestHumidity = item.Humidity;
                foreach (var item2 in arrayWeatherData)
                {
                    if (item2.Date.Date == itemDate)
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
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWeatherData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }

        [RelayCommand]
        public void CalcMeanHumidityWeather()
        {
            int meanHumidityId = 0;
            int meanHumidity = 0;
            List<int[]> outputWeatherData = new List<int[]>(); ;
            Array arrayWeatherData = _context.WeatherData.ToArray();
            foreach (var item in arrayWeatherData)
            {
                Date itemDate = item.Date.Date;
                meanHumidityId = item.Id;
                meanHumidity = item.Humidity;
                foreach (var item2 in arrayWeatherData)
                {
                    if (item2.Date.Date == itemDate)
                    {
                        meanHumidity += item2.Humidity;
                    }
                    else
                    {
                        outputWeatherData.Add(new int[] { meanHumidityId, meanHumidity });
                        break;
                    }
                }
                tableData = new ObservableCollection<AnalyticalTestsEnviromentalViewModel>(outputWeatherData.Select(x => new AnalyticalTestsEnviromentalViewModel
                {
                    ColumnId = x[0],
                    ColumnData = x[1]
                }));
            }
        }


        [RelayCommand]
        public void ReturnToHome()
        {
            Shell.Current.GoToAsync("//AllUsersPage");
        }
    }
}
