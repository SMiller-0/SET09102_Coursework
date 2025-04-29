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

        /*! \brief Test if the CreateCSV method works correctly when the database is populated.
        *
        *  Simply calls the function and checks that no exceptions are thrown.
        */
        [Fact]
        public void CreateUserCSV_FromPopulatedDb_SuccessfulFileCreation()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);

            //Act
            bool csvCreationSuccessful = backupViewModel.CreateCSV();

            //Assert
            Assert.True(csvCreationSuccessful);
        }

        /*! \brief Test if the CopyDatabase method works correctly when the database is populated.
        *
        *  Simply calls the function and checks that no exceptions are thrown.
        */
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

        /*! \brief Test if the LoadData method works correctly when the database is populated.
        *
        * Assigns a random number for the table, which is probably bad practice for a test since they should be invariable,
        * then calls the function and checks that the some data is returned.
        */
        [Fact]
        public void LoadData_UsingRandomTable_DataNotEmpty()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);
            int randomTable = 3; // Use a fixed table index for deterministic behavior

            //Act
            backupViewModel.LoadData();

            //Assert
            Assert.NotEmpty(backupViewModel.AllData);

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
            var backupViewModel = new AdminBackUpViewModel(context);
            int tableNo = 10;
            backupViewModel.currentTable = tableNo;

            //Act
            backupViewModel.LoadData();

            //Assert
            Assert.Empty(backupViewModel.AllData);

        }

        /*! \brief Test if the nextPage method works correctly when the database is populated.
        *
        * A random, valid number is assigned to the current table, with 2 being chosen, and the function is called.
        * This test depends on the LoadData function, making it an integration test rather than a pure unit test.
        * It verifies that nextPage correctly calls LoadData and returns some data.
        */
        [Fact]
        public void nextPage_UsingSecondTable_DataLoaded()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);
            int tableNo = 2;
            backupViewModel.currentTable = tableNo;

            //Act
            backupViewModel.nextPage();

            //Assert
            Assert.NotEmpty(backupViewModel.AllData);

        }

        /*! \brief Test if the previousPage method works correctly when the database is populated.
        *
        * A random, valid number is assigned to the current table, with 2 being chosen, and the function is called.
        * This test depends on the LoadData function, making it an integration test rather than a pure unit test.
        * It verifies that previousPage correctly calls LoadData and returns some data.
        */
        [Fact]
        public void previousPage_UsingFourthTable_DataLoaded()
        {
            //Arrange
            var context = _fixture._testDbContext;
            var backupViewModel = new AdminBackUpViewModel(context);
            int tableNo = 4;
            backupViewModel.currentTable = tableNo;

            //Act
            backupViewModel.previousPage();

            //Assert
            Assert.NotEmpty(backupViewModel.AllData);

        }
    }
}