using System;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="CurrentUserService"/>, covering its role flags,
    /// login/logout behavior, and <see cref="ICurrentUserService.UserChanged"/> events.
    /// </summary>
    public class CurrentUserServiceTests
    {
        /// <summary>
        /// Verifies that a newly constructed service has no user and all role flags are false.
        /// </summary>
        [Fact]
        public void DefaultState_LoggedInUserIsNull_AndAllRoleFlagsFalse()
        {
            // Arrange: A new instance of CurrentUserService
            // Act: No action needed, just check the default state
            var svc = new CurrentUserService();

            // Assert: LoggedInUser is null and all role flags are false
            Assert.Null(svc.LoggedInUser);
            Assert.False(svc.IsAdmin);
            Assert.False(svc.IsOperationsManager);
            Assert.False(svc.IsEnvScientist);
        }

        /// <summary>
        /// Checks that setting a user with various roles toggles the corresponding
        /// IsAdmin, IsOperationsManager, and IsEnvScientist properties correctly.
        /// </summary>
        [Theory]
        [InlineData("Administrator", true, false, false)]
        [InlineData("Operations Manager", false, true, false)]
        [InlineData("Environmental Scientist", false, false, true)]
        [InlineData("SomeOtherRole", false, false, false)]
        public void IsRoleFlags_VaryByLoggedInUserRole(
            string roleName,
            bool expectAdmin,
            bool expectOpsMgr,
            bool expectEnvSci)
        {
            // Arrange: Create a new CurrentUserService and set a user with a specific role
            var svc = new CurrentUserService();
            var user = new User {
                FirstName = "X",
                Role = new Role { RoleName = roleName }
            };

            // Act: Set the user in the service
            svc.SetUser(user);

            // Assert: Check that the role flags match the expected values
            Assert.Equal(expectAdmin, svc.IsAdmin);
            Assert.Equal(expectOpsMgr, svc.IsOperationsManager);
            Assert.Equal(expectEnvSci, svc.IsEnvScientist);
        }

        /// <summary>
        /// Ensures that <see cref="CurrentUserService.SetUser(User)"/> stores the user
        /// and raises the <see cref="ICurrentUserService.UserChanged"/> event.
        /// </summary>
        [Fact]
        public void SetUser_SetsLoggedInUser_AndRaisesUserChanged()
        {
            // Arrange: Create a new CurrentUserService and a user to set
            var svc = new CurrentUserService();
            var user = new User { FirstName = "Jane" };

            // Act: Set the user in the service and check if the event is raised
            bool eventRaised = false;
            svc.UserChanged += (_, __) => eventRaised = true;

            svc.SetUser(user);

            // Assert: Check that the user is set and the event was raised
            Assert.Same(user, svc.LoggedInUser);
            Assert.True(eventRaised);
        }

        /// <summary>
        /// Verifies that <see cref="CurrentUserService.Logout"/> clears the logged-in user
        /// and fires the <see cref="ICurrentUserService.UserChanged"/> event.
        /// </summary>
        [Fact]
        public void Logout_ClearsLoggedInUser_AndRaisesUserChanged()
        {
            // Arrange: Create a new CurrentUserService and set an initial user
            var svc = new CurrentUserService();
            svc.SetUser(new User { FirstName = "Jane" }); // initial state with user

            bool eventRaised = false;
            svc.UserChanged += (_, __) => eventRaised = true;

            // Act: Call Logout to clear the user
            svc.Logout();

            // Assert: Check that the user is cleared and the event was raised
            Assert.Null(svc.LoggedInUser);
            Assert.True(eventRaised);
        }
    }
}
