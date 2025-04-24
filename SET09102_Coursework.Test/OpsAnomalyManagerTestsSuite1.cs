using Notes.Test;
using SET09102_Coursework.Models;
using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Test
{
    public class OpsAnomalyManagerTestsSuite1 : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture _fixture;
        public OpsAnomalyManagerTestsSuite1(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _fixture.Seed();
        }

        /*! \brief Test if the method CheckAirData works correctly when the database is populated.
        *
        *  Uses a test record to check if normal values are flagged correctly as 'Normal' by the CheckAirData method.
        */
        [Fact]
        public void CheckAirData_UsingDummyTestRecord_NormalValues()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var anomalyManagerViewModel = new OpsAnomalyManagerViewModel(context);
            AirQData? testRecord = null; // Will change into a dummy record WITH ACCEPTABLE VALUES IN ALL FIELDS

            //Act
            anomalyManagerViewModel.CheckAirData(testRecord);

            //Assert
            Assert.True(anomalyManagerViewModel.FlagColumn == "Normal");
        }

        /*! \brief Test if the method CheckAirData works correctly when the database is populated.
        *
        *  Uses a test record to check if incorrect or unusual values are flagged correctly as 'Anomaly' by the CheckAirData method.
        */
        [Fact]
        public void CheckAirData_UsingDummyTestRecord_AnomalyValueForNitrogenValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var anomalyManagerViewModel = new OpsAnomalyManagerViewModel(context);
            AirQData? testRecord = null; // Will change into a dummy record with an anomaly in nitrogen value but acceptable values in all other fields

            //Act
            anomalyManagerViewModel.CheckAirData(testRecord);

            //Assert
            Assert.True(anomalyManagerViewModel.FlagColumn == "Anomaly");
        }

        /*! \brief Test if the method CheckWaterData works correctly when the database is populated.
        *
        *  Uses a test record to check if normal values are flagged correctly as 'Normal' by the CheckWaterData method.
        */
        [Fact]
        public void CheckWaterData_UsingDummyTestRecord_NormalValues()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var anomalyManagerViewModel = new OpsAnomalyManagerViewModel(context);
            WaterQData? testRecord = null; // Will change into a dummy record WITH ACCEPTABLE VALUES IN ALL FIELDS

            //Act
            anomalyManagerViewModel.CheckWaterData(testRecord);

            //Assert
            Assert.True(anomalyManagerViewModel.FlagColumn == "Normal");
        }

        /*! \brief Test if the method CheckWaterData works correctly when the database is populated.
        *
        *  Uses a test record to check if incorrect or unusual values are flagged correctly as 'Anomaly' by the CheckWaterData method.
        */
        [Fact]
        public void CheckWaterData_UsingDummyTestRecord_AnomalyValueForPhosphate()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var anomalyManagerViewModel = new OpsAnomalyManagerViewModel(context);
            WaterQData? testRecord = null; // Will change into a dummy record with an anomaly in phosphate but acceptable values in all other fields

            //Act
            anomalyManagerViewModel.CheckWaterData(testRecord);

            //Assert
            Assert.True(anomalyManagerViewModel.FlagColumn == "Anomaly");
        }

        /*! \brief Test if the method CheckWeatherData works correctly when the database is populated.
        *
        *  Uses a test record to check if normal values are flagged correctly as 'Normal' by the CheckWeatherData method.
        */
        [Fact]
        public void CheckWeatherData_UsingDummyTestRecord_NormalValues()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var anomalyManagerViewModel = new OpsAnomalyManagerViewModel(context);
            WeatherData? testRecord = null; // Will change into a dummy record WITH ACCEPTABLE VALUES IN ALL FIELDS
            //Act
            anomalyManagerViewModel.CheckWeatherData(testRecord);

            //Assert
            Assert.True(anomalyManagerViewModel.FlagColumn == "Normal");
        }

        /*! \brief Test if the method CheckWeatherData works correctly when the database is populated.
        *
        *  Uses a test record to check if incorrect or unusual values are flagged correctly as 'Anomaly' by the CheckWeatherData method.
        */
        [Fact]
        public void CheckWeatherData_UsingDummyTestRecord_AnomalyValueForWindSpeed()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var anomalyManagerViewModel = new OpsAnomalyManagerViewModel(context);
            WeatherData? testRecord = null; // Will change into a dummy record with an anomaly in wind speed but acceptable values in all other fields
            //Act
            anomalyManagerViewModel.CheckWeatherData(testRecord);

            //Assert
            Assert.True(anomalyManagerViewModel.FlagColumn == "Anomaly");
        }

        /*! \brief Test if the LoadData method works correctly when the database is populated.
        *
        * Assigns a random numberly chosen, valid number for the table, with 6 being chosen,
        * then calls the function and checks that some data is returned.
        */
        [Fact]
        public void LoadData_UsingSixTable_DataNotEmpty()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var opAnomalyViewModel = new OpsAnomalyManagerViewModel(context);
            int randomTable = 6;
            opAnomalyViewModel.currentTable = randomTable;

            //Act
            opAnomalyViewModel.LoadData();

            //Assert
            Assert.NotEmpty(opAnomalyViewModel.AllData);

        }

        /*! \brief Test if the LoadData method handles out of bound errors correctly.
        *
        * Assigns an out-of-bounds number as the current table to ensure that the error handling works correctly.
        */
        [Fact]
        public void LoadData_UsingOutOfBoundTable_ErrorHandledCorrectly()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var opAnomalyViewModel = new OpsAnomalyManagerViewModel(context);
            int tableNo = 10;
            opAnomalyViewModel.currentTable = tableNo;

            //Act
            opAnomalyViewModel.LoadData();

            //Assert
            Assert.Empty(opAnomalyViewModel.AllData);

        }

        /*! \brief Test if the nextPage method works correctly when the database is populated.
        *
        * A random, valid number is assigned to the current table, with 5 being chosen, and the function is called.
        * Should correctly call the LoadData function and return some data.
        * Possible problem with this is that LoadData is necessary to test this function, so it is not a true unit test.
        */
        [Fact]
        public void nextPage_UsingSecondTable_DataLoaded()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var opAnomalyViewModel = new OpsAnomalyManagerViewModel(context);
            int tableNo = 5;
            opAnomalyViewModel.currentTable = tableNo;

            //Act
            opAnomalyViewModel.nextPage();

            //Assert
            Assert.NotEmpty(opAnomalyViewModel.AllData);

        }

        /*! \brief Test if the previousPage method works correctly when the database is populated.
        *
        * A random, valid number is assigned to the current table, with 3 being chosen, and the function is called.
        * Should correctly call the LoadData function and return some data.
        * Possible problem with this is that LoadData is necessary to test this function, so it is not a true unit test.
        */
        [Fact]
        public void previousPage_UsingThirdTable_DataLoaded()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var opAnomalyViewModel = new OpsAnomalyManagerViewModel(context);
            int tableNo = 3;
            opAnomalyViewModel.currentTable = tableNo;

            //Act
            opAnomalyViewModel.previousPage();

            //Assert
            Assert.NotEmpty(opAnomalyViewModel.AllData);

        }
    }
}