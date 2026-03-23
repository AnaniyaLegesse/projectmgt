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
            v => v.Label,              
            v => Priority.Parse(v)       
        );

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.Property(p => p.Priority)
                .HasConversion(priorityConverter)
                .HasMaxLength(20); 

            
            entity.Property(p => p.Status)
                .HasConversion<string>();
        });
    }
}


