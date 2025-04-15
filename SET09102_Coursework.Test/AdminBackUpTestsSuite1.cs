using Notes.Test;
using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Test
{
    public class AdminBackUpTestsSuite1 : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture _fixture;
        public AdminBackUpTestsSuite1(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _fixture.Seed();
        }

        [Fact]
        public void CreateUserCSV_FromPopulatedDb_SuccessfulFileCreation()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);

            //Act
            bool csvCreattionSuccessful = backupViewModel.CreateCSV();

            //Assert
            Assert.True(csvCreattionSuccessful);
        }

        [Fact]
        public void CopyDatabase_FromPopulatedDb_SuccessfulDatabaseClone()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);

            //Act
            bool databaseCloneSuccessful = backupViewModel.CopyDatabase();

            //Assert
            Assert.True(databaseCloneSuccessful);
        }

        [Fact]
        public void LoadData_UsingRandomTable_DataNotEmpty()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);
            Random rnd = new Random();
            int randomTable = rnd.Next(0, 7);
            backupViewModel.currentTable = randomTable;

            //Act
            backupViewModel.LoadData();

            //Assert
            Assert.NotEmpty(backupViewModel.AllData);

        }
    }
}