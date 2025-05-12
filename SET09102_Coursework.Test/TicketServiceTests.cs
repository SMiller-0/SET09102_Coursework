using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;
using SET09102_Coursework.Services;
using Xunit;

namespace SET09102_Coursework.Test
{
    /// <summary>
    /// Unit tests for <see cref="TicketService"/>.
    /// Verifies that the service can create, update, and delete tickets correctly.
    /// </summary>
    public class TicketServiceTests
    {
        private static DbContextOptions<AppDbContext> NewContextOptions()
            => new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        /// <summary>
        /// CreateTicketAsync should return true and persist the ticket when it is created successfully.
        /// </summary>
        [Fact]
        public async Task CreateTicketAsync_AddsAndReturnsTrue()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            var ticket = new SensorTicket {
                Id               = 1,
                SensorId         = 7,
                StatusId         = 2,
                IssueDescription = "Something broke",
                CreatedAt        = DateTime.UtcNow
            };

            // Act: attempt to create the ticket
            var result = await svc.CreateTicketAsync(ticket);

            // Assert: creation returns true and persisted values match
            Assert.True(result);
            var fromDb = await ctx.SensorTickets.FindAsync(1);
            Assert.NotNull(fromDb);
            Assert.Equal("Something broke", fromDb.IssueDescription);
        }

        /// <summary>
        /// GetAllTicketsAsync should return all tickets ordered by CreatedAt descending.
        /// </summary>
       [Fact]
        public async Task GetAllTicketsAsync_ReturnsOrdered()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.TicketStatuses.AddRange(new[]
                {
                    new TicketStatus { Id = 1, StatusName = "Open"   },
                    new TicketStatus { Id = 2, StatusName = "Closed" }
                });

                seed.SensorTypes.Add(new SensorType { Id = 1, Name = "DummyType" });
                seed.Sensors.Add(new Sensor {
                    Id              = 7,
                    Name            = "TestSensor",
                    FirmwareVersion = "1.1.0",
                    SensorTypeId    = 1
                });

                seed.SensorTickets.AddRange(new[]
                {
                    new SensorTicket {
                        Id               = 1,
                        SensorId         = 7,
                        StatusId         = 2,
                        IssueDescription = "Late",
                        CreatedAt        = DateTime.UtcNow.AddHours(-1)
                    },
                    new SensorTicket {
                        Id               = 2,
                        SensorId         = 7,
                        StatusId         = 1,
                        IssueDescription = "Early",
                        CreatedAt        = DateTime.UtcNow
                    }
                });

                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: retrieve all tickets
            var ids = (await svc.GetAllTicketsAsync())
                .Select(t => t.Id)
                .ToList();

            // Assert: IDs should be in descending order of CreatedAt
            Assert.Equal(new List<int> { 2, 1 }, ids);
        }

        /// <summary>
        /// GetTicketsByStatusAsync should return tickets filtered by status and ordered by CreatedAt descending.
        /// </summary>
        [Fact]
        public async Task GetTicketsByStatusAsync_FiltersAndOrders()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.TicketStatuses.Add(new TicketStatus { Id = 2, StatusName = "Under Investigation" });
                seed.SensorTypes.Add(new SensorType { Id = 1, Name = "DummyType" });
                seed.Sensors.Add(new Sensor {
                    Id              = 3,
                    Name            = "TestSensor",
                    FirmwareVersion = "1.1.0",
                    SensorTypeId    = 1
                });

                seed.SensorTickets.AddRange(new[]
                {
                    new SensorTicket { Id = 1, SensorId = 3, StatusId = 2, IssueDescription = "Old", CreatedAt = DateTime.UtcNow.AddMinutes(-5) },
                    new SensorTicket { Id = 2, SensorId = 3, StatusId = 2, IssueDescription = "New", CreatedAt = DateTime.UtcNow }
                });
                await seed.SaveChangesAsync();   
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: retrieve tickets by status
            var ids = (await svc.GetTicketsByStatusAsync(2))
              .Select(t => t.Id)
              .ToList();

            // Assert: IDs should be in descending order of CreatedAt
            Assert.Equal(new List<int> { 2, 1 }, ids);
        }

        /// <summary>
        /// ChangeTicketStatusAsync should update the status of an existing ticket and return true.
        /// </summary>
        [Fact]
        public async Task ChangeTicketStatusAsync_Existing_ReturnsTrueAndPersists()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.SensorTickets.Add(new SensorTicket { Id = 5, SensorId = 3, StatusId = 1, IssueDescription = "X", CreatedAt = DateTime.UtcNow });
                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: change status of the ticket
            var ok = await svc.ChangeTicketStatusAsync(5, 3);
            Assert.True(ok);

            // Assert: ticket status is updated in the database
            var reloaded = await ctx.SensorTickets.FindAsync(5);
            Assert.Equal(3, reloaded.StatusId);
        }

        /// <summary>
        /// ChangeTicketStatusAsync should return false when the ticket does not exist.
        /// </summary>
        [Fact]
        public async Task ChangeTicketStatusAsync_Nonexistent_ReturnsFalse()
        {
            // Arrange: empty database
            var opts = NewContextOptions();
            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: attempt to change status of a nonexistent ticket
            var ok = await svc.ChangeTicketStatusAsync(999, 2);

            // Assert: returns false
            Assert.False(ok);
        }

        /// <summary>
        /// GetAllTicketStatusesAsync should return all ticket statuses ordered by StatusName.
        /// </summary>
        [Fact]
        public async Task GetAllTicketStatusesAsync_ReturnsSorted()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.TicketStatuses.AddRange(new[]
                {
                    new TicketStatus { Id = 1, StatusName = "Z" },
                    new TicketStatus { Id = 2, StatusName = "A" }
                });
                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: retrieve all ticket statuses
            var names = (await svc.GetAllTicketStatusesAsync())
                        .Select(s => s.StatusName)
                        .ToList();

            Assert.Equal(new List<string> { "A", "Z" }, names);
        }

        /// <summary>
        /// GetTicketByIdAsync should return the ticket with the specified ID, or null if it does not exist.
        /// </summary>
       [Fact]
        public async Task GetTicketByIdAsync_IncludesNavigation()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.TicketStatuses.Add(new TicketStatus { Id = 3, StatusName = "O" });
                seed.SensorTypes.Add(new SensorType { Id = 1, Name = "T" });
                seed.Sensors.Add(new Sensor { Id = 4, Name = "S", FirmwareVersion = "v", Latitude = 0m, Longitude = 0m, SensorTypeId = 1 });
                seed.SensorTickets.Add(new SensorTicket { Id = 9, SensorId = 4, StatusId = 3, IssueDescription = "Nav", CreatedAt = DateTime.UtcNow });
                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: retrieve the ticket by ID
            var ticket = await svc.GetTicketByIdAsync(9);

            // Assert: ticket should not be null and should include navigation properties
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Status);
            Assert.NotNull(ticket.Sensor);
        }

        /// <summary>
        /// AddTicketResponseAsync should add a new ticket response and return true.
        /// </summary>
        [Fact]
        public async Task AddTicketResponseAsync_AddsAndReturnsTrue()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);
            var resp = new TicketResponse { Id = 1, TicketId = 7, StatusId = 1, CreatedAt = DateTime.UtcNow };
            
            // Act: add the ticket response
            var ok = await svc.AddTicketResponseAsync(resp);

            // Assert: addition returns true and persisted values match
            Assert.True(ok);
            Assert.Single(ctx.TicketResponses);
        }

        /// <summary>
        /// GetTicketResponsesAsync should return all responses for a specific ticket, ordered by CreatedAt descending.
        /// </summary>
        [Fact]
        public async Task GetTicketResponsesAsync_FiltersAndOrders()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
             {
                seed.TicketStatuses.Add(new TicketStatus { Id = 1, StatusName = "SomeStatus" });
                seed.TicketResponses.AddRange(
                    new TicketResponse { Id = 1, TicketId = 7, StatusId = 1, CreatedAt = DateTime.UtcNow.AddMinutes(-2) },
                    new TicketResponse { Id = 2, TicketId = 7, StatusId = 1, CreatedAt = DateTime.UtcNow.AddMinutes(-1) });
                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: retrieve ticket responses for a specific ticket
            var ids = (await svc.GetTicketResponsesAsync(7))
                      .Select(r => r.Id)
                      .ToList();

            // Assert: IDs should be in descending order of CreatedAt
            Assert.Equal(new List<int> { 1, 2 }, ids);
        }

        /// <summary>
        /// DeleteTicketAsync should remove the ticket and return true if it exists.
        /// </summary>
        [Fact]
        public async Task DeleteTicketAsync_Existing_RemovesAndFiresEvent()
        {
            // Arrange: seed
            var opts = NewContextOptions();
            using (var seed = new AppDbContext(opts))
            {
                seed.SensorTickets.Add(new SensorTicket { Id = 5, SensorId = 1, StatusId = 1, IssueDescription = "X", CreatedAt = DateTime.UtcNow });
                await seed.SaveChangesAsync();
            }

            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            int? firedId = null;
            svc.TicketDeleted += id => firedId = id;

            // Act: delete the ticket
            var ok = await svc.DeleteTicketAsync(5);

            // Assert: deletion returns true, ticket is removed, and event is fired
            Assert.True(ok);
            Assert.Null(await ctx.SensorTickets.FindAsync(5));
            Assert.Equal(5, firedId);
        }

        /// <summary>
        /// DeleteTicketAsync should return false if the ticket does not exist.
        /// </summary>
        [Fact]
        public async Task DeleteTicketAsync_Nonexistent_ReturnsFalse()
        {
            // Arrange: empty database
            var opts = NewContextOptions();
            using var ctx = new AppDbContext(opts);
            var svc = new TicketService(ctx);

            // Act: attempt to delete a nonexistent ticket
            var ok = await svc.DeleteTicketAsync(9999);
            
            // Assert: returns false
            Assert.False(ok);
        }
    }
}
