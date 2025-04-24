using Notes.Test;
using SET09102_Coursework.Models;
using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Test
{
    public class AnalyticalTestsEnviromentalTestsSuite1 : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture _fixture;
        public AnalyticalTestsEnviromentalTestsSuite1(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _fixture.Seed();
        }

        /*! \brief Test if the method CalcHighestNitrogenAir works correctly.
        *
        *  Uses the test database to check if the highest nitrogen value is calculated correctly, as the first value in the test database is constant.
        */
        [Fact]
        public void CalcHighestNitrogenAir_BasedOnTestDB_CorrectHighestValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var analyticalViewModel = new AnalyticalTestsEnviromentalViewModel(context);

            //Act
            analyticalViewModel.CalcHighestNitrogenAir();
            AirQData testRecord = (AirQData)analyticalViewModel.AllData[0];

            //Assert
            Assert.True(testRecord.Nitrogen == 10);
        }

        /*! \brief Test if the method CalcLowestNitrateWater works correctly.
        *
        *  Uses the test database to check if the lowest nitrate value is calculated correctly, as the first value in the test database is constant.
        */
        [Fact]
        public void CalcLowestNitrateWater_BasedOnTestDB_CorrectLowestValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var analyticalViewModel = new AnalyticalTestsEnviromentalViewModel(context);

            //Act
            analyticalViewModel.CalcLowestNitrateWater();
            WaterQData testRecord = (WaterQData)analyticalViewModel.AllData[0];

            //Assert
            Assert.True(testRecord.Nitrate == 1);
        }

        /*! \brief Test if the method CalcMeanHumidityWeather works correctly.
        *
        *  Uses the test database to check if the mean humidity value is calculated correctly, as the first value in the test database is constant.
        */
        [Fact]
        public void CalcMeanHumidityWeather_BasedOnTestDB_CorrectMeanValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var analyticalViewModel = new AnalyticalTestsEnviromentalViewModel(context);

            //Act
            analyticalViewModel.CalcMeanHumidityWeather();
            WeatherData testRecord = (WeatherData)analyticalViewModel.AllData[0];

            //Assert
            Assert.True(testRecord.Humidity == 10);
        }
    }
}