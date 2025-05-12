using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for the <see cref="LoginViewModel"/>'s input validation logic.
    /// </summary>
    public class LoginViewModelTests
    {
        /// <summary>
        /// When Email is empty but Password is provided,
        /// LoginCommand must fail immediately with the “required” error
        /// and never call SetUser().
        /// </summary>
        [Fact]
        public async Task LoginCommand_EmptyEmail_SetsRequiredFieldError()
        {
            // Arrange: missing email, valid password
            var options     = new DbContextOptions<AppDbContext>();
            var mockCtx     = new Mock<AppDbContext>(options);
            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new LoginViewModel(mockCtx.Object, mockUserSvc.Object)
            {
                Email    = "",
                Password = "validPassword"
            };

            // Act: execute the login command
            await vm.LoginCommand.ExecuteAsync(null);

            // Assert: login failed with correct message; SetUser was not called
            Assert.True(vm.IsLoginFailed);
            Assert.Equal("Email and password are required.", vm.LoginError);
            mockUserSvc.Verify(s => s.SetUser(It.IsAny<User>()), Times.Never);
        }

        /// <summary>
        /// When Password is empty but Email is provided,
        /// LoginCommand must fail immediately with the “required” error
        /// and never call SetUser().
        /// </summary>
        [Fact]
        public async Task LoginCommand_EmptyPassword_SetsRequiredFieldError()
        {
            // Arrange: valid email, missing password
            var options     = new DbContextOptions<AppDbContext>();
            var mockCtx     = new Mock<AppDbContext>(options);
            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new LoginViewModel(mockCtx.Object, mockUserSvc.Object)
            {
                Email    = "user@example.com",
                Password = ""
            };

            // Act: execute the login command
            await vm.LoginCommand.ExecuteAsync(null);

            // Assert: login failed with correct message; SetUser was not called
            Assert.True(vm.IsLoginFailed);
            Assert.Equal("Email and password are required.", vm.LoginError);
            mockUserSvc.Verify(s => s.SetUser(It.IsAny<User>()), Times.Never);
        }

        /// <summary>
        /// When both Email and Password are empty or whitespace,
        /// LoginCommand must fail with the “required” error
        /// and never call SetUser().
        /// </summary>
        [Theory]
        [InlineData("", "")]
        [InlineData("   ", "   ")]
        public async Task LoginCommand_BothEmptyOrWhitespace_SetsRequiredFieldError(string email, string password)
        {
            // Arrange: both fields empty or whitespace
            var options     = new DbContextOptions<AppDbContext>();
            var mockCtx     = new Mock<AppDbContext>(options);
            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new LoginViewModel(mockCtx.Object, mockUserSvc.Object)
            {
                Email    = email,
                Password = password
            };

            // Act: execute the login command
            await vm.LoginCommand.ExecuteAsync(null);

            // Assert: login failed with correct message; SetUser was not called
            Assert.True(vm.IsLoginFailed);
            Assert.Equal("Email and password are required.", vm.LoginError);
            mockUserSvc.Verify(s => s.SetUser(It.IsAny<User>()), Times.Never);
        }

        /// <summary>
        /// When Email format is invalid (does not match regex),
        /// LoginCommand must fail with the “invalid email” error
        /// and never call SetUser().
        /// </summary>
        [Fact]
        public async Task LoginCommand_InvalidEmailFormat_SetsFormatError()
        {
            // Arrange: malformed email, valid password
            var options     = new DbContextOptions<AppDbContext>();
            var mockCtx     = new Mock<AppDbContext>(options);
            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new LoginViewModel(mockCtx.Object, mockUserSvc.Object)
            {
                Email    = "not-an-email",
                Password = "Password123!"
            };

            // Act: execute the login command
            await vm.LoginCommand.ExecuteAsync(null);

            // Assert: login failed with correct format‐error message; SetUser was not called
            Assert.True(vm.IsLoginFailed);
            Assert.Equal("Please enter a valid email address.", vm.LoginError);
            mockUserSvc.Verify(s => s.SetUser(It.IsAny<User>()), Times.Never);
        }
    }
}
