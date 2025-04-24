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
    /*! \brief The code-behind for AdminBackUpView.xaml to meet the requirements of Issue #10.*/
    public partial class AdminBackUpViewModel : ObservableObject
    {
        /*! \brief Creates a model of the application's associated database using Data/AppDbContext class.*/
        private readonly AppDbContext _context;

        /*! \brief Creates an index to keep track of which table is currently being displayed.*/
        public int currentTable = 0;

        /*! \brief List of tables to display, which needs to be manually updated as new tables are added to the database.*/
        public string[] tableNames = { "Users", "Roles", "Sensors", "SensorTypes", "AirQData", "WaterQData", "WeatherData" };

        /*! \brief The two following variables, name & currentDate, are used when creating a name for the database backup or csv file to allow for multiple backups in the same location
        * Also creates a sort of version-control system for backups so long as users regularly create backups.*/
        public string name = "Admin_DB_Backup";
        public System.DateTime currentDate = DateTime.Now.Date;

        /*! \brief This collection is used to display the correct data to users.
         * Due to the use of different data models depending on table, a generic object is used as the type.*/
        public ObservableCollection<System.Object> AllData { get; } = new();

        /*! \brief The constructor for this ViewModel.
        *
        *  Creates a model of the database using the Data/AppDbContext class and calls the LoadData() method to display the first table of data.
        */
        public AdminBackUpViewModel(AppDbContext context)
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
            if (currentTable + 1 < tableNames.Length - 1)
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

        /*! \brief A function that creates a clone of the application's associated database for redundancy purposes.
        *
        *  Creates a copy of the database used by the application using the libraries Microsoft.Data.SqlClient and Microsoft.Extensions.Configuration.
        *  The new database is named using the name variable and the current date to allow for multiple backups in the same location and a form of version control.
        */
        [RelayCommand]
        public bool CopyDatabase()
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

        /*! \brief A function that creates a comma-separated values (CSV) file of the current table of data for archiving and backup.
        *
        *  Creates a csv file of the current table displayed from the database associated with the application.
        *  The csv is named using the name variable and the current date to allow for multiple backups in the same location and a form of version control.
        */
        [RelayCommand]
        public bool CreateCSV()
        {
            try
            {
                string csvName = name + "_" + tableNames[currentTable] + "_" + currentDate.ToString("yyyyMMdd");
                string fileName = csvName + ".csv";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
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


        /*! \brief A function that loads the correct data from the database for the current table index.
        *
        *  Removes the information currently displayed in the table onscreen and replaces it with the data from the database associated with the current table index.
        */
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
            else
            {
                /*! \brief An error message for if the table index would go out of bounds.*/
                Shell.Current.DisplayAlert("Error! Out of bounds table value.",
                    $"If this issue persists, please contact our customer support team.", "OK");
                currentTable = 0;
            }
        }
    }
}