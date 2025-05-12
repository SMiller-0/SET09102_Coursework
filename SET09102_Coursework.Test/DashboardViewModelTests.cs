using System;
using System.ComponentModel;
using Moq;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.ViewModels;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="DashboardViewModel"/>.
    /// Verifies that the welcome message is generated correctly
    /// and that the PropertyChanged event fires when the user changes.
    /// </summary>
    public class DashboardViewModelTests
    {
        /// <summary>
        /// If no user is logged in (<see cref="ICurrentUserService.LoggedInUser"/> is null),
        /// <see cref="DashboardViewModel.WelcomeMessage"/> should fall back to the generic greeting.
        /// </summary>
        [Fact]
        public void WelcomeMessage_NoUser_ReturnsGenericGreeting()
        {
            // Arrange: LoggedInUser is null
            var mockUserSvc = new Mock<ICurrentUserService>();
            mockUserSvc.SetupGet(s => s.LoggedInUser).Returns((User?)null);

            var vm = new DashboardViewModel(mockUserSvc.Object);

            // Act & Assert
            Assert.Equal("Welcome!", vm.WelcomeMessage);
        }

        /// <summary>
        /// If a user is logged in, <see cref="DashboardViewModel.WelcomeMessage"/>
        /// should include their <see cref="User.FirstName"/> in a personalized greeting.
        /// </summary>
        [Fact]
        public void WelcomeMessage_WithUser_ReturnsPersonalGreeting()
        {
            // Arrange: LoggedInUser has a FirstName
            var user = new User { FirstName = "Alice" };
            var mockUserSvc = new Mock<ICurrentUserService>();
            mockUserSvc.SetupGet(s => s.LoggedInUser).Returns(user);

            var vm = new DashboardViewModel(mockUserSvc.Object);

            // Act & Assert
            Assert.Equal("Welcome, Alice!", vm.WelcomeMessage);
        }

        /// <summary>
        /// When the underlying service raises its <see cref="ICurrentUserService.UserChanged"/> event,
        /// the view-model should fire <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// for <see cref="DashboardViewModel.WelcomeMessage"/> and update the greeting.
        /// </summary>
        [Fact]
        public void When_UserChanged_RaisesPropertyChanged_ForWelcomeMessage()
        {
            // Arrange
            var user = new User { FirstName = "Bob" };
            var mockUserSvc = new Mock<ICurrentUserService>();
            mockUserSvc.SetupGet(s => s.LoggedInUser).Returns(user);

            var vm = new DashboardViewModel(mockUserSvc.Object);

            bool eventFired = false;
            vm.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(DashboardViewModel.WelcomeMessage))
                    eventFired = true;
            };

            // Act: simulate the service raising its UserChanged event
            mockUserSvc.Raise(s => s.UserChanged += null, mockUserSvc.Object, EventArgs.Empty);

            // Assert
            Assert.True(eventFired, "OnUserChanged should trigger PropertyChanged for WelcomeMessage");
            Assert.Equal("Welcome, Bob!", vm.WelcomeMessage);
        }
    }
}
