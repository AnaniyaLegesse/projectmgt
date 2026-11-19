using Domain.Organizations;
using Domain.Projects;
using Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskEntity = Domain.Tasks.Task;
using TaskStatusEntity = Domain.Tasks.TaskStatus;

namespace Persistence.Configurations;

internal static class TaskConfigurations
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var task = modelBuilder.Entity<TaskEntity>();
        task.ToTable("Tasks");
        task.HasKey(entity => entity.Id);
        task.Property(entity => entity.Title).HasMaxLength(250).IsRequired();
        task.Property(entity => entity.Description).HasMaxLength(4000).IsRequired();
        task.Property(entity => entity.SortKey).HasMaxLength(100).IsRequired();
        task.Property(entity => entity.Estimate).HasPrecision(18, 4);
        task.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        task.HasOne(entity => entity.Phase).WithMany().HasForeignKey(entity => entity.PhaseId);
        task.HasOne(entity => entity.Milestone).WithMany().HasForeignKey(entity => entity.MilestoneId);
        task.HasOne(entity => entity.ParentTask).WithMany().HasForeignKey(entity => entity.ParentTaskId);
        task.HasOne(entity => entity.Status).WithMany().HasForeignKey(entity => entity.StatusId);
        task.HasOne(entity => entity.Priority).WithMany().HasForeignKey(entity => entity.PriorityId);
        task.HasOne(entity => entity.Reporter).WithMany().HasForeignKey(entity => entity.ReporterId);
        task.HasIndex(entity => new { entity.ProjectId, entity.SortKey });
        task.HasIndex(entity => new { entity.ProjectId, entity.StatusId });

        var taskStatus = modelBuilder.Entity<TaskStatusEntity>();
        taskStatus.ToTable("TaskStatuses");
        taskStatus.HasKey(entity => entity.Id);
        taskStatus.Property(entity => entity.Name).HasMaxLength(100).IsRequired();
        taskStatus.Property(entity => entity.Catagories).HasMaxLength(100).IsRequired();
        taskStatus.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();
        taskStatus.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);

        var priority = modelBuilder.Entity<TaskPriorities>();
        priority.ToTable("TaskPriorities");
        priority.HasKey(entity => entity.Id);
        priority.Property(entity => entity.Name).HasMaxLength(100).IsRequired();
        priority.Property(entity => entity.Color).HasMaxLength(30).IsRequired();
        priority.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();
        priority.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);

        var dependency = modelBuilder.Entity<TaskDependency>();
        dependency.ToTable("TaskDependencies");
        dependency.HasKey(entity => entity.Id);
        dependency.Property(entity => entity.Type).HasMaxLength(50).IsRequired();
        dependency.HasIndex(entity => new { entity.PredecessorTaskId, entity.SuccessorTaskId }).IsUnique();
        dependency.HasOne(entity => entity.PredecessorTask).WithMany().HasForeignKey(entity => entity.PredecessorTaskId);
        dependency.HasOne(entity => entity.SuccessorTask).WithMany().HasForeignKey(entity => entity.SuccessorTaskId);

        var board = modelBuilder.Entity<Board>();
        board.ToTable("Boards");
        board.HasKey(entity => entity.Id);
        board.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        board.Property(entity => entity.BoardType).HasMaxLength(50).IsRequired();
        board.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        board.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();

        var boardColumn = modelBuilder.Entity<BoardColumn>();
        boardColumn.ToTable("BoardColumns", table => table.HasCheckConstraint(
            "CK_BoardColumns_NonNegativeWipLimit",
            "[WipLimit] >= 0"));
        boardColumn.HasKey(entity => entity.Id);
        boardColumn.Property(entity => entity.Name).HasMaxLength(150).IsRequired();
        boardColumn.HasOne(entity => entity.Board).WithMany().HasForeignKey(entity => entity.BoardId);
        boardColumn.HasIndex(entity => new { entity.BoardId, entity.SortOrder }).IsUnique();

        var boardTask = modelBuilder.Entity<BoardTask>();
        boardTask.ToTable("BoardTasks");
        boardTask.HasKey(entity => entity.Id);
        boardTask.Property(entity => entity.Rank).HasMaxLength(100).IsRequired();
        boardTask.HasOne(entity => entity.Board).WithMany().HasForeignKey(entity => entity.BoardId);
        boardTask.HasOne(entity => entity.Column).WithMany().HasForeignKey(entity => entity.ColumnId);
        boardTask.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);
        boardTask.HasIndex(entity => new { entity.BoardId, entity.TaskId }).IsUnique();

        var sprint = modelBuilder.Entity<Sprint>();
        sprint.ToTable("Sprints");
        sprint.HasKey(entity => entity.Id);
        sprint.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        sprint.Property(entity => entity.Goal).HasMaxLength(2000).IsRequired();
        sprint.Property(entity => entity.Status).HasMaxLength(50).IsRequired();
        sprint.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        sprint.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();

        var sprintTask = modelBuilder.Entity<SprintTask>();
        sprintTask.ToTable("SprintTasks");
        sprintTask.HasKey(entity => entity.Id);
        sprintTask.HasIndex(entity => new { entity.SprintId, entity.TaskId }).IsUnique();
        sprintTask.HasOne(entity => entity.Sprint).WithMany().HasForeignKey(entity => entity.SprintId);
        sprintTask.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);

        var sprintReport = modelBuilder.Entity<SprintReport>();
        sprintReport.ToTable("SprintReports");
        sprintReport.HasKey(entity => entity.Id);
        sprintReport.Property(entity => entity.CommitteddPoints).HasPrecision(18, 4);
        sprintReport.Property(entity => entity.CompletedPoints).HasPrecision(18, 4);
        sprintReport.Property(entity => entity.Velocity).HasPrecision(18, 4);
        sprintReport.HasOne(entity => entity.Sprint).WithMany().HasForeignKey(entity => entity.SprintId);
        sprintReport.HasIndex(entity => entity.SprintId).IsUnique();

        var retrospective = modelBuilder.Entity<ProjectRetrospective>();
        retrospective.ToTable("ProjectRetrospectives");
        retrospective.HasKey(entity => entity.Id);
        retrospective.Property(entity => entity.Title).HasMaxLength(250).IsRequired();
        retrospective.Property(entity => entity.Notes).HasMaxLength(4000).IsRequired();
        retrospective.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        retrospective.HasOne(entity => entity.Sprint).WithMany().HasForeignKey(entity => entity.SprintId);
        retrospective.HasIndex(entity => entity.SprintId);

        var label = modelBuilder.Entity<Label>();
        label.ToTable("Labels");
        label.HasKey(entity => entity.Id);
        label.Property(entity => entity.Name).HasMaxLength(100).IsRequired();
        label.Property(entity => entity.Color).HasMaxLength(30).IsRequired();
        label.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        label.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();

        var taskLabel = modelBuilder.Entity<TaskLabel>();
        taskLabel.ToTable("TaskLabels");
        taskLabel.HasKey(entity => entity.Id);
        taskLabel.HasIndex(entity => new { entity.TaskId, entity.LabelId }).IsUnique();
        taskLabel.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);
        taskLabel.HasOne(entity => entity.Label).WithMany().HasForeignKey(entity => entity.LabelId);

        var taskAssignee = modelBuilder.Entity<TaskAssignee>();
        taskAssignee.ToTable("TaskAssignees");
        taskAssignee.HasKey(entity => entity.Id);
        taskAssignee.HasIndex(entity => new { entity.TaskId, entity.ProjectMemberId }).IsUnique();
        taskAssignee.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);
        taskAssignee.HasOne(entity => entity.ProjectMember).WithMany().HasForeignKey(entity => entity.ProjectMemberId);

        var taskWatcher = modelBuilder.Entity<TaskWatcher>();
        taskWatcher.ToTable("TaskWatchers");
        taskWatcher.HasKey(entity => entity.Id);
        taskWatcher.HasIndex(entity => new { entity.TaskId, entity.UserId }).IsUnique();
        taskWatcher.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);
        taskWatcher.HasOne(entity => entity.User).WithMany().HasForeignKey(entity => entity.UserId);

        var goalTaskLink = modelBuilder.Entity<GoalTaskLink>();
        goalTaskLink.ToTable("GoalTaskLinks");
        goalTaskLink.HasKey(entity => entity.Id);
        goalTaskLink.Property(entity => entity.Weight).HasPrecision(9, 4);
        goalTaskLink.HasIndex(entity => new { entity.GoalId, entity.TaskId }).IsUnique();
        goalTaskLink.HasOne(entity => entity.Goal).WithMany().HasForeignKey(entity => entity.GoalId);
        goalTaskLink.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);

        var baseline = modelBuilder.Entity<ProjectBaseline>();
        baseline.ToTable("ProjectBaselines");
        baseline.HasKey(entity => entity.Id);
        baseline.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        baseline.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        baseline.HasOne(entity => entity.CreatedBy).WithMany().HasForeignKey(entity => entity.CreatedById);

        var baselineItem = modelBuilder.Entity<BaselineItem>();
        baselineItem.ToTable("BaselineItems");
        baselineItem.HasKey(entity => entity.Id);
        baselineItem.Property(entity => entity.PlannedEffort).HasPrecision(18, 4);
        baselineItem.HasIndex(entity => new { entity.BaselineId, entity.TaskId }).IsUnique();
        baselineItem.HasOne(entity => entity.Baseline).WithMany().HasForeignKey(entity => entity.BaselineId);
        baselineItem.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);

        var customFieldDefinition = modelBuilder.Entity<CustomFieldDefinition>();
        customFieldDefinition.ToTable("CustomFieldDefinitions");
        customFieldDefinition.HasKey(entity => entity.Id);
        customFieldDefinition.Property(entity => entity.Name).HasMaxLength(150).IsRequired();
        customFieldDefinition.Property(entity => entity.FieldType).HasMaxLength(50).IsRequired();
        customFieldDefinition.Property(entity => entity.Options).HasMaxLength(4000);
        customFieldDefinition.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        customFieldDefinition.HasIndex(entity => new { entity.ProjectId, entity.Name }).IsUnique();

        var customFieldValue = modelBuilder.Entity<CustomFieldValue>();
        customFieldValue.ToTable("CustomFieldValues");
        customFieldValue.HasKey(entity => entity.Id);
        customFieldValue.Property(entity => entity.Value).HasColumnType("nvarchar(max)").IsRequired();
        customFieldValue.HasOne(entity => entity.Task).WithMany().HasForeignKey(entity => entity.TaskId);
        customFieldValue.HasOne(entity => entity.FieldDefinition).WithMany().HasForeignKey(entity => entity.FieldDefinitionId);
        customFieldValue.HasIndex(entity => new { entity.TaskId, entity.FieldDefinitionId }).IsUnique();

        var savedView = modelBuilder.Entity<SavedView>();
        savedView.ToTable("SavedViews");
        savedView.HasKey(entity => entity.Id);
        savedView.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        savedView.Property(entity => entity.ViewType).HasMaxLength(50).IsRequired();
        savedView.Property(entity => entity.FilterJson).HasColumnType("nvarchar(max)").IsRequired();
        savedView.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        savedView.HasOne(entity => entity.Owner).WithMany().HasForeignKey(entity => entity.OwnerId);
        savedView.HasIndex(entity => new { entity.ProjectId, entity.OwnerId, entity.Name }).IsUnique();

        var workflowTransition = modelBuilder.Entity<WorkflowTransition>();
        workflowTransition.ToTable("WorkflowTransitions");
        workflowTransition.HasKey(entity => entity.Id);
        workflowTransition.Property(entity => entity.Conditions).HasColumnType("nvarchar(max)").IsRequired();
        workflowTransition.HasOne(entity => entity.Project).WithMany().HasForeignKey(entity => entity.ProjectId);
        workflowTransition.HasOne(entity => entity.FromStatus).WithMany().HasForeignKey(entity => entity.FromStatusId).OnDelete(DeleteBehavior.NoAction);
        workflowTransition.HasOne(entity => entity.ToStatus).WithMany().HasForeignKey(entity => entity.ToStatusId).OnDelete(DeleteBehavior.NoAction);
        workflowTransition.HasIndex(entity => new { entity.ProjectId, entity.FromStatusId, entity.ToStatusId }).IsUnique();
    }
}
