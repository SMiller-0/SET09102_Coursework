using Microsoft.EntityFrameworkCore;
using SET09102_Coursework.Data;
using SET09102_Coursework.Models;

namespace Notes.Test;

public class DatabaseFixture
{
    internal TestDbContext? _testDbContext { get; private set; }

    public DatabaseFixture()
    {
        _testDbContext = new TestDbContext();

        _testDbContext.Database.EnsureDeleted();
        _testDbContext.Database.EnsureCreated();
        _testDbContext.Database.OpenConnection();
        _testDbContext.Database.Migrate();
    }

    internal void Seed()
    {
        var project = new Database.Models.Project();
        project.Name = "Seed project";
        project.Description = "Just testing";
        project.StartDate = new DateTime(2025, 1, 10, 9, 00, 00);
        project.EndDate = new DateTime(2025, 10, 31, 17, 00, 00);

        _testDbContext.Add(project);
        _testDbContext.SaveChanges();

        /*var note = new Note();
        note.Date = DateTime.Now;
        note.Text = "Seed note";
        note.Project = project;

        _testDbContext.Add(note);
        _testDbContext.SaveChanges();*/
    }

}

