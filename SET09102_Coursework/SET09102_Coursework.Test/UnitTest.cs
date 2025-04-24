namespace SET09102_Coursework.Test
{
    public class UnitTest
    {
        [Fact]
        public void TestDefault()
        {
            // Arrange: Set up mocks for dependancies and any other necessary objects.
            bool mockVariable = true;
            // Act: Perform the action you want to test.
            var testVariable = mockVariable;
            // Assert: Check the results against an absolute to ensure it matches expectations.
            Assert.True(testVariable);
        }
    }
}