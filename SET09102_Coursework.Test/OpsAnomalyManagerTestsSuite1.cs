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
    }
}