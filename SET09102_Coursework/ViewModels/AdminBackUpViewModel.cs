using CommunityToolkit.Mvvm.ComponentModel;
using SET09102_Coursework.Models;
using SET09102_Coursework.Data;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace SET09102_Coursework.ViewModels
{
    public partial class AdminBackUpViewModel : ObservableObject
    {
        public int currentTable = 0;

        public string[] tableNames = { "Users", "Roles", "Sensors", "SensorTypes", "AirQData", "WaterQData", "WeatherData" };

        public string name = "Admin_DB_Backup";
        public System.DateTime currentDate = DateTime.Now.Date;

        private readonly AppDbContext _context;
        public ObservableCollection<System.Object> AllData { get; } = new();

        public AdminBackUpViewModel(AppDbContext context)
        {
            _context = context;
            LoadData();
        }


        [RelayCommand]
        public void nextPage()
        {
            if (currentTable < 6)
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

        [RelayCommand]
        public bool copyDatabase()
        {
            try
            {
                string database = "courseworkdb";
                string databaseName = name + "_" + currentDate.ToString("yyyyMMdd");
                var commandText = "CREATE DATABASE " + databaseName + "AS COPY OF " + database;
                string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new System.String[] { @"bin\" }, StringSplitOptions.None)[0];
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("LocalConnection");

                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(commandText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Shell.Current.DisplayAlert("Success", "Data exported to backup database successfully.", "OK");
                return true;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", $"Failed to export data: {ex.Message}", "OK");
            }
            return false;
        }

        [RelayCommand]
        public bool createCSV()
        {
            try
            {
                string csvName = name + "_" + tableNames[currentTable] + "_" + currentDate.ToString("yyyyMMdd");
                string fileName = csvName + ".csv";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var item in AllData)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
                Shell.Current.DisplayAlert("Success", "Data exported to CSV successfully.", "OK");
                return true;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", $"Failed to export data: {ex.Message}", "OK");
            }
            return false;
        }



        public void LoadData()
        {
            if (currentTable == 0)
            {
                var currentData = _context.Users.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else if (currentTable == 1)
            {
                var currentData = _context.Roles.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else if (currentTable == 2)
            {
                var currentData = _context.Sensors.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else if (currentTable == 3)
            {
                var currentData = _context.SensorTypes.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else if (currentTable == 4)
            {
                var currentData = _context.AirQData.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else if (currentTable == 5)
            {
                var currentData = _context.WaterQData.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
            else if (currentTable == 6)
            {
                var currentData = _context.WeatherData.ToList();
                AllData.Clear();
                foreach (var info in currentData)
                {
                    AllData.Add(info);
                }
            }
        }
    }
}