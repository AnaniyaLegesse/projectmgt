using Domain.Organizations;
using Domain.Projects;
using Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using TaskEntity = Domain.Tasks.Task;
using TaskStatusEntity = Domain.Tasks.TaskStatus;

namespace Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<OrganizationMember> OrganizationMembers => Set<OrganizationMember>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<MemberRole> MemberRoles => Set<MemberRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
    public DbSet<SSOConnection> SSOConnections => Set<SSOConnection>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<ResourcePermission> ResourcePermissions => Set<ResourcePermission>();

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectPhase> ProjectPhases => Set<ProjectPhase>();
    public DbSet<ProjectMilestone> ProjectMilestones => Set<ProjectMilestone>();
    public DbSet<ProjectDeliverable> ProjectDeliverables => Set<ProjectDeliverable>();
    public DbSet<ProjectGoal> ProjectGoals => Set<ProjectGoal>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();

    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    public DbSet<TaskStatusEntity> TaskStatuses => Set<TaskStatusEntity>();
    public DbSet<TaskPriorities> TaskPriorities => Set<TaskPriorities>();
    public DbSet<TaskDependency> TaskDependencies => Set<TaskDependency>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<BoardColumn> BoardColumns => Set<BoardColumn>();
    public DbSet<BoardTask> BoardTasks => Set<BoardTask>();
    public DbSet<Sprint> Sprints => Set<Sprint>();
    public DbSet<SprintTask> SprintTasks => Set<SprintTask>();
    public DbSet<SprintReport> SprintReports => Set<SprintReport>();
    public DbSet<ProjectRetrospective> ProjectRetrospectives => Set<ProjectRetrospective>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<TaskLabel> TaskLabels => Set<TaskLabel>();
    public DbSet<TaskAssignee> TaskAssignees => Set<TaskAssignee>();
    public DbSet<TaskWatcher> TaskWatchers => Set<TaskWatcher>();
    public DbSet<GoalTaskLink> GoalTaskLinks => Set<GoalTaskLink>();
    public DbSet<ProjectBaseline> ProjectBaselines => Set<ProjectBaseline>();
    public DbSet<BaselineItem> BaselineItems => Set<BaselineItem>();
    public DbSet<CustomFieldDefinition> CustomFieldDefinitions => Set<CustomFieldDefinition>();
    public DbSet<CustomFieldValue> CustomFieldValues => Set<CustomFieldValue>();
    public DbSet<SavedView> SavedViews => Set<SavedView>();
    public DbSet<WorkflowTransition> WorkflowTransitions => Set<WorkflowTransition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        OrganizationConfigurations.Configure(modelBuilder);
        ProjectConfigurations.Configure(modelBuilder);
        TaskConfigurations.Configure(modelBuilder);

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(entityType => entityType.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}


