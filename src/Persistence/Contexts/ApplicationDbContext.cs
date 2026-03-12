using Domain.Entities;
using Domain.ValueObj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectTask> Tasks => Set<ProjectTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var priorityConverter = new ValueConverter<Priority, string>(
            v => v.ToString(),            // store -> db
            v => Priority.Parse(v)        // db -> CLR (implement Parse accordingly)
        );

        modelBuilder.Entity<ProjectTask>()
            .Property(p => p.Priority)
            .HasConversion(priorityConverter)
            .HasMaxLength(50);
    }
}


