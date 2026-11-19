using Domain.Organizations;
using Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations;

internal static class ProjectConfigurations
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var project = modelBuilder.Entity<Project>();
        project.ToTable("Projects");
        project.HasKey(entity => entity.Id);
        project.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        project.Property(entity => entity.Key).HasMaxLength(50).IsRequired();
        project.Property(entity => entity.Status).HasMaxLength(50).IsRequired();
        project.HasIndex(entity => new { entity.OrganizationId, entity.Key }).IsUnique();
        project.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);
        project.HasOne(entity => entity.Owner).WithMany().HasForeignKey(entity => entity.OwnerId);

        var phase = modelBuilder.Entity<ProjectPhase>();
        phase.ToTable("ProjectPhases");
        phase.HasKey(entity => entity.Id);
        phase.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        phase.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();
        phase.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);

        var milestone = modelBuilder.Entity<ProjectMilestone>();
        milestone.ToTable("ProjectMilestones");
        milestone.HasKey(entity => entity.Id);
        milestone.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        milestone.Property(entity => entity.Status).HasMaxLength(50);
        milestone.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        milestone.HasOne(entity => entity.Phase).WithMany().HasForeignKey(entity => entity.PhaseId);
        milestone.HasIndex(entity => new { entity.ProjectId, entity.Name });

        var deliverable = modelBuilder.Entity<ProjectDeliverable>();
        deliverable.ToTable("ProjectDeliverables");
        deliverable.HasKey(entity => entity.Id);
        deliverable.Property(entity => entity.Name).HasMaxLength(250).IsRequired();
        deliverable.Property(entity => entity.Status).HasMaxLength(50).IsRequired();
        deliverable.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        deliverable.HasOne(entity => entity.Milestone).WithMany().HasForeignKey(entity => entity.MilestoneId);
        deliverable.HasIndex(entity => new { entity.ProjectId, entity.MilestoneId });

        var goal = modelBuilder.Entity<ProjectGoal>();
        goal.ToTable("ProjectGoals");
        goal.HasKey(entity => entity.Id);
        goal.Property(entity => entity.Title).HasMaxLength(250).IsRequired();
        goal.Property(entity => entity.Status).HasMaxLength(50).IsRequired();
        goal.Property(entity => entity.TargetValue).HasPrecision(18, 4);
        goal.Property(entity => entity.CurrentValue).HasPrecision(18, 4);
        goal.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        goal.HasOne(entity => entity.Parent).WithMany().HasForeignKey(entity => entity.ParentId);
        goal.HasIndex(entity => new { entity.ProjectId, entity.ParentId });

        var member = modelBuilder.Entity<ProjectMember>();
        member.ToTable("ProjectMembers");
        member.HasKey(entity => entity.Id);
        member.Property(entity => entity.Role).HasMaxLength(100).IsRequired();
        member.HasIndex(entity => new { entity.ProjectId, entity.UserId }).IsUnique();
        member.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        member.HasOne(entity => entity.User).WithMany().HasForeignKey(entity => entity.UserId);
    }
}
