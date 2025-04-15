using Notes.Test;
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

        [Fact]
        public void CalcHighestNitrogenAir_BasedOnTestDB_CorrectHighestValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var analyticalViewModel = new AnalyticalTestsEnviromentalViewModel(context);

            //Act
            analyticalViewModel.CalcHighestNitrogenAir();

            //Assert
            Assert.True(analyticalViewModel.tableData[0].Nitrogen == 10);
        }

        [Fact]
        public void CalcLowestNitrateWater_BasedOnTestDB_CorrectLowestValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var analyticalViewModel = new AnalyticalTestsEnviromentalViewModel(context);

            //Act
            analyticalViewModel.CalcLowestNitrateWater();

            //Assert
            Assert.True(analyticalViewModel.tableData[0].Nitrate == 1);
        }

        [Fact]
        public void CalcMeanHumidityWeather_BasedOnTestDB_CorrectMeanValue()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var analyticalViewModel = new AnalyticalTestsEnviromentalViewModel(context);

            //Act
            analyticalViewModel.CalcMeanHumidityWeather();

            //Assert
            Assert.True(analyticalViewModel.tableData[0].Humidity == 10);
        }
    }
}