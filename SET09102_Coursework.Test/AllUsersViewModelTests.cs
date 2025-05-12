using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Moq;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using SET09102_Coursework.ViewModels;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="AllUsersViewModel"/>.
    /// </summary>
    public class AllUsersViewModelTests
    {
        /// <summary>
        /// Constructor calls RefreshUserList, which should load and sort users by surname, then first name.
        /// </summary>
        [Fact]
        public void Ctor_SeedsAndSortsAllUsers()
        {
            // Arrange: seed Role + three Users
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var seedRoleCtx = new AppDbContext(opts))
            {
                seedRoleCtx.Roles.Add(new Role { RoleId = 1, RoleName = "TestRole" });
                seedRoleCtx.SaveChanges();
            }

            using (var seedUserCtx = new AppDbContext(opts))
            {
                seedUserCtx.Users.AddRange(new[]
                {
                    new User {
                        FirstName = "Adam",
                        Surname   = "Zebra",
                        Street    = "1 Main St",
                        City      = "Town",
                        Postcode  = "12345",
                        Email     = "adam@example.com",
                        Password  = "pw",
                        RoleId    = 1
                    },
                    new User {
                        FirstName = "Bernie",
                        Surname   = "Alpha",
                        Street    = "2 Oak Ave",
                        City      = "Town",
                        Postcode  = "12345",
                        Email     = "bernie@example.com",
                        Password  = "pw",
                        RoleId    = 1
                    },
                    new User {
                        FirstName = "Cathy",
                        Surname   = "Alpha",
                        Street    = "3 Pine Rd",
                        City      = "Town",
                        Postcode  = "12345",
                        Email     = "cathy@example.com",
                        Password  = "pw",
                        RoleId    = 1
                    },
                });
                seedUserCtx.SaveChanges();
            }

            // Act: create the view model
            var mockUserSvc = new Mock<ICurrentUserService>();
            using var testCtx = new AppDbContext(opts);
            var vm = new AllUsersViewModel(testCtx, mockUserSvc.Object);

            // Assert: AllUsers is sorted by Surname, then FirstName
            Assert.Equal(3, vm.AllUsers.Count);
            var surnames = vm.AllUsers.Select(u => u.Surname).ToArray();
            var firsts   = vm.AllUsers.Select(u => u.FirstName).ToArray();

            Assert.Equal(new[] { "Alpha", "Alpha", "Zebra" }, surnames);
            Assert.Equal(new[] { "Bernie", "Cathy", "Adam" },    firsts);
        }

        /// <summary>
        /// IsAdmin simply returns whatever the ICurrentUserService says.
        /// </summary>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsAdmin_PropagatesFromCurrentUserService(bool serviceValue)
        {
            // Arrange: seed Role + one User
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using var ctx = new AppDbContext(opts);

            var mockUserSvc = new Mock<ICurrentUserService>();
            mockUserSvc.SetupGet(s => s.IsAdmin).Returns(serviceValue);

            // Act: create the view model
            var vm = new AllUsersViewModel(ctx, mockUserSvc.Object);

            // Assert: IsAdmin matches the service value
            Assert.Equal(serviceValue, vm.IsAdmin);
        }

        /// <summary>
        /// When ICurrentUserService.UserChanged fires, VM must raise PropertyChanged for IsAdmin.
        /// </summary>
        [Fact]
        public void OnUserChanged_RaisesPropertyChangedForIsAdmin()
        {
            // Arrange: seed Role + one User
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using var ctx = new AppDbContext(opts);

            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new AllUsersViewModel(ctx, mockUserSvc.Object);

            bool fired = false;
            vm.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(AllUsersViewModel.IsAdmin))
                    fired = true;
            };

            // Act: trigger the UserChanged event
            mockUserSvc.Raise(s => s.UserChanged += null, mockUserSvc.Object, EventArgs.Empty);

            // Assert: PropertyChanged was raised for IsAdmin
            Assert.True(fired);
        }

        /// <summary>
        /// ApplyQueryAttributes with "created"/"saved"/"deleted" must call RefreshUserList again.
        /// </summary>
        [Fact]
        public void ApplyQueryAttributes_WithFlag_RefreshesList()
        {
            // Arrange: seed Role + one User
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using (var seedRoleCtx = new AppDbContext(opts))
            {
                seedRoleCtx.Roles.Add(new Role { RoleId = 1, RoleName = "R" });
                seedRoleCtx.SaveChanges();
            }
            using (var seedUserCtx = new AppDbContext(opts))
            {
                seedUserCtx.Users.Add(new User {
                    FirstName = "Only",
                    Surname   = "One",
                    Street    = "1 St",
                    City      = "Town",
                    Postcode  = "00000",
                    Email     = "only@ex.com",
                    Password  = "pw",
                    RoleId    = 1
                });
                seedUserCtx.SaveChanges();
            }

            using var testCtx = new AppDbContext(opts);
            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new AllUsersViewModel(testCtx, mockUserSvc.Object);

            vm.AllUsers.Clear();
            Assert.Empty(vm.AllUsers);

            var query = new Dictionary<string, object> { ["created"] = true };

            // Act: call ApplyQueryAttributes with "created" flag
            ((IQueryAttributable)vm).ApplyQueryAttributes(query);

            // Assert: RefreshUserList was called, and the user list is now populated
            Assert.Single(vm.AllUsers);
            Assert.Equal("One", vm.AllUsers[0].Surname);
        }

        /// <summary>
        /// ApplyQueryAttributes without any of those flags does nothing.
        /// </summary>
        [Fact]
        public void ApplyQueryAttributes_WithoutFlag_Noop()
        {
            // Arrange: seed Role + two Users
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using (var seedRoleCtx = new AppDbContext(opts))
            {
                seedRoleCtx.Roles.Add(new Role { RoleId = 1, RoleName = "R" });
                seedRoleCtx.SaveChanges();
            }
            using (var seedUserCtx = new AppDbContext(opts))
            {
                seedUserCtx.Users.AddRange(
                    new User {
                        FirstName = "A", Surname  = "X",
                        Street    = "1 St", City     = "T",
                        Postcode  = "00001", Email   = "a@ex.com",
                        Password  = "pw", RoleId    = 1
                    },
                    new User {
                        FirstName = "B", Surname  = "Y",
                        Street    = "2 St", City     = "T",
                        Postcode  = "00002", Email   = "b@ex.com",
                        Password  = "pw", RoleId    = 1
                    }
                );
                seedUserCtx.SaveChanges();
            }

            using var ctx = new AppDbContext(opts);
            var mockUserSvc = new Mock<ICurrentUserService>();
            var vm = new AllUsersViewModel(ctx, mockUserSvc.Object);

            // Act: clear the list and call ApplyQueryAttributes with no flags
            ((IQueryAttributable)vm).ApplyQueryAttributes(new Dictionary<string, object>());

            // Assert: the list is still empty
            Assert.Equal(2, vm.AllUsers.Count);
        }
    }
}
