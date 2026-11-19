IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Organizations] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [slug] nvarchar(100) NOT NULL,
        [plan] nvarchar(50) NULL,
        [Status] nvarchar(50) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Organizations] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Permissions] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(150) NOT NULL,
        [Description] nvarchar(500) NULL,
        CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(200) NOT NULL,
        [Email] nvarchar(320) NOT NULL,
        [PasswordHash] nvarchar(512) NULL,
        [AvatarUrl] nvarchar(2048) NULL,
        [timezone] nvarchar(100) NULL,
        [status] nvarchar(50) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Scope] nvarchar(100) NULL,
        [IsSystem] bit NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Roles_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [SSOConnections] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [Protocol] nvarchar(50) NOT NULL,
        [Domain] nvarchar(255) NOT NULL,
        [Config] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_SSOConnections] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SSOConnections_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Teams] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [Status] nvarchar(50) NULL,
        [Description] nvarchar(2000) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Teams] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Teams_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ApiKeys] (
        [Id] int NOT NULL IDENTITY,
        [KeyHash] nvarchar(512) NOT NULL,
        [OrganizationId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [Scope] nvarchar(max) NULL,
        [ExpiredAt] datetime2 NULL,
        [LastUsedAt] datetime2 NULL,
        [CreatedById] int NOT NULL,
        CONSTRAINT [PK_ApiKeys] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApiKeys_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]),
        CONSTRAINT [FK_ApiKeys_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [AuditLogs] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [ActorId] int NOT NULL,
        [Action] nvarchar(150) NOT NULL,
        [TargetType] nvarchar(150) NOT NULL,
        [TargetId] int NOT NULL,
        [MetaData] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AuditLogs_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]),
        CONSTRAINT [FK_AuditLogs_Users_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [OrganizationMembers] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [UserId] int NOT NULL,
        [Status] nvarchar(50) NULL,
        [JoinedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_OrganizationMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrganizationMembers_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]),
        CONSTRAINT [FK_OrganizationMembers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [Key] nvarchar(50) NOT NULL,
        [Status] nvarchar(50) NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [ActualEndDate] datetime2 NULL,
        [OwnerId] int NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Projects_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]),
        CONSTRAINT [FK_Projects_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [RolePermissions] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] int NOT NULL,
        [PermissionId] int NOT NULL,
        CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]),
        CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ResourcePermissions] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationId] int NOT NULL,
        [ResourceType] nvarchar(100) NOT NULL,
        [ResourceId] int NOT NULL,
        [AccessLevel] nvarchar(50) NOT NULL,
        [UserId] int NULL,
        [TeamId] int NULL,
        CONSTRAINT [PK_ResourcePermissions] PRIMARY KEY ([Id]),
        CONSTRAINT [CK_ResourcePermissions_ExactlyOnePrincipal] CHECK (([UserId] IS NOT NULL AND [TeamId] IS NULL) OR ([UserId] IS NULL AND [TeamId] IS NOT NULL)),
        CONSTRAINT [FK_ResourcePermissions_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]),
        CONSTRAINT [FK_ResourcePermissions_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id]),
        CONSTRAINT [FK_ResourcePermissions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TeamMembers] (
        [Id] int NOT NULL IDENTITY,
        [TeamId] int NOT NULL,
        [UserId] int NOT NULL,
        [Role] nvarchar(100) NOT NULL,
        [Status] nvarchar(50) NULL,
        [JoinedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TeamMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TeamMembers_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id]),
        CONSTRAINT [FK_TeamMembers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [MemberRoles] (
        [Id] int NOT NULL IDENTITY,
        [OrganizationMemberId] int NOT NULL,
        [RoleId] int NOT NULL,
        [AssignedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_MemberRoles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MemberRoles_OrganizationMembers_OrganizationMemberId] FOREIGN KEY ([OrganizationMemberId]) REFERENCES [OrganizationMembers] ([Id]),
        CONSTRAINT [FK_MemberRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Boards] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [BoardType] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Boards] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Boards_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [CustomFieldDefinitions] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [FieldType] nvarchar(50) NOT NULL,
        [Options] nvarchar(4000) NULL,
        [IsRequired] bit NOT NULL,
        CONSTRAINT [PK_CustomFieldDefinitions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CustomFieldDefinitions_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Labels] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Color] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_Labels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Labels_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectBaselines] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [CreatedById] int NOT NULL,
        [CapturedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectBaselines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectBaselines_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
        CONSTRAINT [FK_ProjectBaselines_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectGoals] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [ParentId] int NULL,
        [Title] nvarchar(250) NOT NULL,
        [TargetValue] decimal(18,4) NOT NULL,
        [CurrentValue] decimal(18,4) NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [Status] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_ProjectGoals] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectGoals_ProjectGoals_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [ProjectGoals] ([Id]),
        CONSTRAINT [FK_ProjectGoals_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectMembers] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [UserId] int NOT NULL,
        [Role] nvarchar(100) NOT NULL,
        [JoinedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectMembers_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
        CONSTRAINT [FK_ProjectMembers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectPhases] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [SortOrder] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectPhases] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectPhases_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [SavedViews] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [OwnerId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [ViewType] nvarchar(50) NOT NULL,
        [FilterJson] nvarchar(max) NOT NULL,
        [IsShared] bit NOT NULL,
        CONSTRAINT [PK_SavedViews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SavedViews_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
        CONSTRAINT [FK_SavedViews_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Sprints] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [Goal] nvarchar(2000) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [Status] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Sprints] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Sprints_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TaskPriorities] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Color] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_TaskPriorities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaskPriorities_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TaskStatuses] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Catagories] nvarchar(100) NOT NULL,
        [SortOrder] int NOT NULL,
        CONSTRAINT [PK_TaskStatuses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaskStatuses_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [BoardColumns] (
        [Id] int NOT NULL IDENTITY,
        [BoardId] int NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [SortOrder] int NOT NULL,
        [WipLimit] int NOT NULL,
        CONSTRAINT [PK_BoardColumns] PRIMARY KEY ([Id]),
        CONSTRAINT [CK_BoardColumns_NonNegativeWipLimit] CHECK ([WipLimit] >= 0),
        CONSTRAINT [FK_BoardColumns_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Boards] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectMilestones] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [PhaseId] int NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [Status] nvarchar(50) NULL,
        CONSTRAINT [PK_ProjectMilestones] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectMilestones_ProjectPhases_PhaseId] FOREIGN KEY ([PhaseId]) REFERENCES [ProjectPhases] ([Id]),
        CONSTRAINT [FK_ProjectMilestones_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectRetrospectives] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [SprintId] int NOT NULL,
        [Title] nvarchar(250) NOT NULL,
        [Notes] nvarchar(4000) NOT NULL,
        [HeldAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectRetrospectives] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectRetrospectives_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
        CONSTRAINT [FK_ProjectRetrospectives_Sprints_SprintId] FOREIGN KEY ([SprintId]) REFERENCES [Sprints] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [SprintReports] (
        [Id] int NOT NULL IDENTITY,
        [SprintId] int NOT NULL,
        [CommitteddPoints] decimal(18,4) NOT NULL,
        [CompletedPoints] decimal(18,4) NOT NULL,
        [Velocity] decimal(18,4) NOT NULL,
        [GeneratedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_SprintReports] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SprintReports_Sprints_SprintId] FOREIGN KEY ([SprintId]) REFERENCES [Sprints] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [WorkflowTransitions] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [FromStatusId] int NOT NULL,
        [ToStatusId] int NOT NULL,
        [Conditions] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_WorkflowTransitions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WorkflowTransitions_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
        CONSTRAINT [FK_WorkflowTransitions_TaskStatuses_FromStatusId] FOREIGN KEY ([FromStatusId]) REFERENCES [TaskStatuses] ([Id]),
        CONSTRAINT [FK_WorkflowTransitions_TaskStatuses_ToStatusId] FOREIGN KEY ([ToStatusId]) REFERENCES [TaskStatuses] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [ProjectDeliverables] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [MilestoneId] int NOT NULL,
        [Name] nvarchar(250) NOT NULL,
        [Status] nvarchar(50) NOT NULL,
        [DueDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectDeliverables] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectDeliverables_ProjectMilestones_MilestoneId] FOREIGN KEY ([MilestoneId]) REFERENCES [ProjectMilestones] ([Id]),
        CONSTRAINT [FK_ProjectDeliverables_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [Tasks] (
        [Id] int NOT NULL IDENTITY,
        [ProjectId] int NOT NULL,
        [PhaseId] int NOT NULL,
        [MilestoneId] int NOT NULL,
        [ParentTaskId] int NULL,
        [StatusId] int NOT NULL,
        [PriorityId] int NOT NULL,
        [ReporterId] int NOT NULL,
        [Title] nvarchar(250) NOT NULL,
        [Description] nvarchar(4000) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [Estimate] decimal(18,4) NULL,
        [SortKey] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tasks_ProjectMilestones_MilestoneId] FOREIGN KEY ([MilestoneId]) REFERENCES [ProjectMilestones] ([Id]),
        CONSTRAINT [FK_Tasks_ProjectPhases_PhaseId] FOREIGN KEY ([PhaseId]) REFERENCES [ProjectPhases] ([Id]),
        CONSTRAINT [FK_Tasks_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
        CONSTRAINT [FK_Tasks_TaskPriorities_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [TaskPriorities] ([Id]),
        CONSTRAINT [FK_Tasks_TaskStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [TaskStatuses] ([Id]),
        CONSTRAINT [FK_Tasks_Tasks_ParentTaskId] FOREIGN KEY ([ParentTaskId]) REFERENCES [Tasks] ([Id]),
        CONSTRAINT [FK_Tasks_Users_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [BaselineItems] (
        [Id] int NOT NULL IDENTITY,
        [BaselineId] int NOT NULL,
        [TaskId] int NOT NULL,
        [PlannedStartDate] datetime2 NOT NULL,
        [PlannedEndDate] datetime2 NOT NULL,
        [PlannedEffort] decimal(18,4) NOT NULL,
        CONSTRAINT [PK_BaselineItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BaselineItems_ProjectBaselines_BaselineId] FOREIGN KEY ([BaselineId]) REFERENCES [ProjectBaselines] ([Id]),
        CONSTRAINT [FK_BaselineItems_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [BoardTasks] (
        [Id] int NOT NULL IDENTITY,
        [BoardId] int NOT NULL,
        [ColumnId] int NOT NULL,
        [TaskId] int NOT NULL,
        [Rank] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_BoardTasks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BoardTasks_BoardColumns_ColumnId] FOREIGN KEY ([ColumnId]) REFERENCES [BoardColumns] ([Id]),
        CONSTRAINT [FK_BoardTasks_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Boards] ([Id]),
        CONSTRAINT [FK_BoardTasks_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [CustomFieldValues] (
        [Id] int NOT NULL IDENTITY,
        [TaskId] int NOT NULL,
        [FieldDefinitionId] int NOT NULL,
        [Value] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_CustomFieldValues] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CustomFieldValues_CustomFieldDefinitions_FieldDefinitionId] FOREIGN KEY ([FieldDefinitionId]) REFERENCES [CustomFieldDefinitions] ([Id]),
        CONSTRAINT [FK_CustomFieldValues_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [GoalTaskLinks] (
        [Id] int NOT NULL IDENTITY,
        [GoalId] int NOT NULL,
        [TaskId] int NOT NULL,
        [Weight] decimal(9,4) NULL,
        CONSTRAINT [PK_GoalTaskLinks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GoalTaskLinks_ProjectGoals_GoalId] FOREIGN KEY ([GoalId]) REFERENCES [ProjectGoals] ([Id]),
        CONSTRAINT [FK_GoalTaskLinks_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [SprintTasks] (
        [Id] int NOT NULL IDENTITY,
        [SprintId] int NOT NULL,
        [TaskId] int NOT NULL,
        [AddedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_SprintTasks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SprintTasks_Sprints_SprintId] FOREIGN KEY ([SprintId]) REFERENCES [Sprints] ([Id]),
        CONSTRAINT [FK_SprintTasks_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TaskAssignees] (
        [Id] int NOT NULL IDENTITY,
        [TaskId] int NOT NULL,
        [ProjectMemberId] int NOT NULL,
        [AssignedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TaskAssignees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaskAssignees_ProjectMembers_ProjectMemberId] FOREIGN KEY ([ProjectMemberId]) REFERENCES [ProjectMembers] ([Id]),
        CONSTRAINT [FK_TaskAssignees_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TaskDependencies] (
        [Id] int NOT NULL IDENTITY,
        [PredecessorTaskId] int NOT NULL,
        [SuccessorTaskId] int NOT NULL,
        [Type] nvarchar(50) NOT NULL,
        [LagMinutes] int NOT NULL,
        CONSTRAINT [PK_TaskDependencies] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaskDependencies_Tasks_PredecessorTaskId] FOREIGN KEY ([PredecessorTaskId]) REFERENCES [Tasks] ([Id]),
        CONSTRAINT [FK_TaskDependencies_Tasks_SuccessorTaskId] FOREIGN KEY ([SuccessorTaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TaskLabels] (
        [Id] int NOT NULL IDENTITY,
        [TaskId] int NOT NULL,
        [LabelId] int NOT NULL,
        CONSTRAINT [PK_TaskLabels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaskLabels_Labels_LabelId] FOREIGN KEY ([LabelId]) REFERENCES [Labels] ([Id]),
        CONSTRAINT [FK_TaskLabels_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE TABLE [TaskWatchers] (
        [Id] int NOT NULL IDENTITY,
        [TaskId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_TaskWatchers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaskWatchers_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]),
        CONSTRAINT [FK_TaskWatchers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ApiKeys_CreatedById] ON [ApiKeys] ([CreatedById]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ApiKeys_KeyHash] ON [ApiKeys] ([KeyHash]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ApiKeys_OrganizationId] ON [ApiKeys] ([OrganizationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_ActorId] ON [AuditLogs] ([ActorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_OrganizationId_CreatedAt] ON [AuditLogs] ([OrganizationId], [CreatedAt]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_BaselineItems_BaselineId_TaskId] ON [BaselineItems] ([BaselineId], [TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_BaselineItems_TaskId] ON [BaselineItems] ([TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_BoardColumns_BoardId_SortOrder] ON [BoardColumns] ([BoardId], [SortOrder]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Boards_ProjectId_Name] ON [Boards] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_BoardTasks_BoardId_TaskId] ON [BoardTasks] ([BoardId], [TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_BoardTasks_ColumnId] ON [BoardTasks] ([ColumnId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_BoardTasks_TaskId] ON [BoardTasks] ([TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustomFieldDefinitions_ProjectId_Name] ON [CustomFieldDefinitions] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_CustomFieldValues_FieldDefinitionId] ON [CustomFieldValues] ([FieldDefinitionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustomFieldValues_TaskId_FieldDefinitionId] ON [CustomFieldValues] ([TaskId], [FieldDefinitionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_GoalTaskLinks_GoalId_TaskId] ON [GoalTaskLinks] ([GoalId], [TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_GoalTaskLinks_TaskId] ON [GoalTaskLinks] ([TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Labels_ProjectId_Name] ON [Labels] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_MemberRoles_OrganizationMemberId_RoleId] ON [MemberRoles] ([OrganizationMemberId], [RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_MemberRoles_RoleId] ON [MemberRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_OrganizationMembers_OrganizationId_UserId] ON [OrganizationMembers] ([OrganizationId], [UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_OrganizationMembers_UserId] ON [OrganizationMembers] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Organizations_slug] ON [Organizations] ([slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Permissions_Code] ON [Permissions] ([Code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectBaselines_CreatedById] ON [ProjectBaselines] ([CreatedById]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectBaselines_ProjectId] ON [ProjectBaselines] ([ProjectId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectDeliverables_MilestoneId] ON [ProjectDeliverables] ([MilestoneId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectDeliverables_ProjectId_MilestoneId] ON [ProjectDeliverables] ([ProjectId], [MilestoneId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectGoals_ParentId] ON [ProjectGoals] ([ParentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectGoals_ProjectId_ParentId] ON [ProjectGoals] ([ProjectId], [ParentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ProjectMembers_ProjectId_UserId] ON [ProjectMembers] ([ProjectId], [UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectMembers_UserId] ON [ProjectMembers] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectMilestones_PhaseId] ON [ProjectMilestones] ([PhaseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectMilestones_ProjectId_Name] ON [ProjectMilestones] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ProjectPhases_ProjectId_Name] ON [ProjectPhases] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectRetrospectives_ProjectId] ON [ProjectRetrospectives] ([ProjectId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ProjectRetrospectives_SprintId] ON [ProjectRetrospectives] ([SprintId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Projects_OrganizationId_Key] ON [Projects] ([OrganizationId], [Key]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Projects_OwnerId] ON [Projects] ([OwnerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ResourcePermissions_OrganizationId_ResourceType_ResourceId] ON [ResourcePermissions] ([OrganizationId], [ResourceType], [ResourceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ResourcePermissions_TeamId] ON [ResourcePermissions] ([TeamId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_ResourcePermissions_UserId] ON [ResourcePermissions] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_RolePermissions_PermissionId] ON [RolePermissions] ([PermissionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_RolePermissions_RoleId_PermissionId] ON [RolePermissions] ([RoleId], [PermissionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Roles_OrganizationId_Name] ON [Roles] ([OrganizationId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_SavedViews_OwnerId] ON [SavedViews] ([OwnerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SavedViews_ProjectId_OwnerId_Name] ON [SavedViews] ([ProjectId], [OwnerId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SprintReports_SprintId] ON [SprintReports] ([SprintId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Sprints_ProjectId_Name] ON [Sprints] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SprintTasks_SprintId_TaskId] ON [SprintTasks] ([SprintId], [TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_SprintTasks_TaskId] ON [SprintTasks] ([TaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SSOConnections_OrganizationId_Domain] ON [SSOConnections] ([OrganizationId], [Domain]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_TaskAssignees_ProjectMemberId] ON [TaskAssignees] ([ProjectMemberId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaskAssignees_TaskId_ProjectMemberId] ON [TaskAssignees] ([TaskId], [ProjectMemberId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaskDependencies_PredecessorTaskId_SuccessorTaskId] ON [TaskDependencies] ([PredecessorTaskId], [SuccessorTaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_TaskDependencies_SuccessorTaskId] ON [TaskDependencies] ([SuccessorTaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_TaskLabels_LabelId] ON [TaskLabels] ([LabelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaskLabels_TaskId_LabelId] ON [TaskLabels] ([TaskId], [LabelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaskPriorities_ProjectId_Name] ON [TaskPriorities] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_MilestoneId] ON [Tasks] ([MilestoneId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_ParentTaskId] ON [Tasks] ([ParentTaskId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_PhaseId] ON [Tasks] ([PhaseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_PriorityId] ON [Tasks] ([PriorityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_ProjectId_SortKey] ON [Tasks] ([ProjectId], [SortKey]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_ProjectId_StatusId] ON [Tasks] ([ProjectId], [StatusId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_ReporterId] ON [Tasks] ([ReporterId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Tasks_StatusId] ON [Tasks] ([StatusId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaskStatuses_ProjectId_Name] ON [TaskStatuses] ([ProjectId], [Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaskWatchers_TaskId_UserId] ON [TaskWatchers] ([TaskId], [UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_TaskWatchers_UserId] ON [TaskWatchers] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TeamMembers_TeamId_UserId] ON [TeamMembers] ([TeamId], [UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_TeamMembers_UserId] ON [TeamMembers] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_Teams_OrganizationId] ON [Teams] ([OrganizationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_WorkflowTransitions_FromStatusId] ON [WorkflowTransitions] ([FromStatusId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE UNIQUE INDEX [IX_WorkflowTransitions_ProjectId_FromStatusId_ToStatusId] ON [WorkflowTransitions] ([ProjectId], [FromStatusId], [ToStatusId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    CREATE INDEX [IX_WorkflowTransitions_ToStatusId] ON [WorkflowTransitions] ([ToStatusId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261119122231_InitialDomainSchema'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261119122231_InitialDomainSchema', N'10.0.13');
END;

COMMIT;
GO

