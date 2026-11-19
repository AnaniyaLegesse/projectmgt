using Domain.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations;

internal static class OrganizationConfigurations
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.HasKey(entity => entity.Id);
        user.Property(entity => entity.FullName).HasMaxLength(200).IsRequired();
        user.Property(entity => entity.Email).HasMaxLength(320).IsRequired();
        user.HasIndex(entity => entity.Email).IsUnique();
        user.Property(entity => entity.PasswordHash).HasMaxLength(512);
        user.Property(entity => entity.AvatarUrl).HasMaxLength(2048);
        user.Property(entity => entity.timezone).HasMaxLength(100);
        user.Property(entity => entity.status).HasMaxLength(50);

        var organization = modelBuilder.Entity<Organization>();
        organization.ToTable("Organizations");
        organization.HasKey(entity => entity.Id);
        organization.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        organization.Property(entity => entity.slug).HasMaxLength(100).IsRequired();
        organization.Property(entity => entity.plan).HasMaxLength(50);
        organization.Property(entity => entity.Status).HasMaxLength(50);
        organization.HasIndex(entity => entity.slug).IsUnique();

        var member = modelBuilder.Entity<OrganizationMember>();
        member.ToTable("OrganizationMembers");
        member.HasKey(entity => entity.Id);
        member.Property(entity => entity.Status).HasMaxLength(50);
        member.HasIndex(entity => new { entity.OrganizationId, entity.UserId }).IsUnique();
        member.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);
        member.HasOne(entity => entity.User).WithMany().HasForeignKey(entity => entity.UserId);

        var team = modelBuilder.Entity<Team>();
        team.ToTable("Teams");
        team.HasKey(entity => entity.Id);
        team.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        team.Property(entity => entity.Status).HasMaxLength(50);
        team.Property(entity => entity.Description).HasMaxLength(2000);
        team.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);

        var teamMember = modelBuilder.Entity<TeamMember>();
        teamMember.ToTable("TeamMembers");
        teamMember.HasKey(entity => entity.Id);
        teamMember.Property(entity => entity.Role).HasMaxLength(100).IsRequired();
        teamMember.Property(entity => entity.Status).HasMaxLength(50);
        teamMember.HasIndex(entity => new { entity.TeamId, entity.UserId }).IsUnique();
        teamMember.HasOne(entity => entity.Team).WithMany().HasForeignKey(entity => entity.TeamId);
        teamMember.HasOne(entity => entity.User).WithMany().HasForeignKey(entity => entity.UserId);

        var role = modelBuilder.Entity<Role>();
        role.ToTable("Roles");
        role.HasKey(entity => entity.Id);
        role.Property(entity => entity.Name).HasMaxLength(100).IsRequired();
        role.Property(entity => entity.Scope).HasMaxLength(100);
        role.HasIndex(entity => new { entity.OrganizationId, entity.Name }).IsUnique();
        role.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);

        var permission = modelBuilder.Entity<Permission>();
        permission.ToTable("Permissions");
        permission.HasKey(entity => entity.Id);
        permission.Property(entity => entity.Code).HasMaxLength(150).IsRequired();
        permission.Property(entity => entity.Description).HasMaxLength(500);
        permission.HasIndex(entity => entity.Code).IsUnique();

        var memberRole = modelBuilder.Entity<MemberRole>();
        memberRole.ToTable("MemberRoles");
        memberRole.HasKey(entity => entity.Id);
        memberRole.HasIndex(entity => new { entity.OrganizationMemberId, entity.RoleId }).IsUnique();
        memberRole.HasOne(entity => entity.OrganizationMember).WithMany().HasForeignKey(entity => entity.OrganizationMemberId);
        memberRole.HasOne(entity => entity.Role).WithMany().HasForeignKey(entity => entity.RoleId);

        var rolePermission = modelBuilder.Entity<RolePermission>();
        rolePermission.ToTable("RolePermissions");
        rolePermission.HasKey(entity => entity.Id);
        rolePermission.HasIndex(entity => new { entity.RoleId, entity.PermissionId }).IsUnique();
        rolePermission.HasOne(entity => entity.Role).WithMany().HasForeignKey(entity => entity.RoleId);
        rolePermission.HasOne(entity => entity.Permission).WithMany().HasForeignKey(entity => entity.PermissionId);

        var apiKey = modelBuilder.Entity<ApiKey>();
        apiKey.ToTable("ApiKeys");
        apiKey.HasKey(entity => entity.Id);
        apiKey.Property(entity => entity.KeyHash).HasMaxLength(512).IsRequired();
        apiKey.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        apiKey.Property(entity => entity.Scope).HasColumnType("nvarchar(max)");
        apiKey.HasIndex(entity => entity.KeyHash).IsUnique();
        apiKey.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);
        apiKey.HasOne(entity => entity.CreatedBy).WithMany().HasForeignKey(entity => entity.CreatedById);

        var ssoConnection = modelBuilder.Entity<SSOConnection>();
        ssoConnection.ToTable("SSOConnections");
        ssoConnection.HasKey(entity => entity.Id);
        ssoConnection.Property(entity => entity.Protocol).HasMaxLength(50).IsRequired();
        ssoConnection.Property(entity => entity.Domain).HasMaxLength(255).IsRequired();
        ssoConnection.Property(entity => entity.Config).HasColumnType("nvarchar(max)").IsRequired();
        ssoConnection.HasIndex(entity => new { entity.OrganizationId, entity.Domain }).IsUnique();
        ssoConnection.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);

        var auditLog = modelBuilder.Entity<AuditLog>();
        auditLog.ToTable("AuditLogs");
        auditLog.HasKey(entity => entity.Id);
        auditLog.Property(entity => entity.Action).HasMaxLength(150).IsRequired();
        auditLog.Property(entity => entity.TargetType).HasMaxLength(150).IsRequired();
        auditLog.Property(entity => entity.MetaData).HasColumnType("nvarchar(max)").IsRequired();
        auditLog.HasOne(entity => entity.Organization).WithMany().HasForeignKey(entity => entity.OrganizationId);
        auditLog.HasOne(entity => entity.Actor).WithMany().HasForeignKey(entity => entity.ActorId);
        auditLog.HasIndex(entity => new { entity.OrganizationId, entity.CreatedAt });

        var resourcePermission = modelBuilder.Entity<ResourcePermission>();
        resourcePermission.ToTable("ResourcePermissions", table => table.HasCheckConstraint(
            "CK_ResourcePermissions_ExactlyOnePrincipal",
            "([UserId] IS NOT NULL AND [TeamId] IS NULL) OR ([UserId] IS NULL AND [TeamId] IS NOT NULL)"));
        resourcePermission.HasKey(entity => entity.Id);
        resourcePermission.Property(entity => entity.ResourceType).HasMaxLength(100).IsRequired();
        resourcePermission.Property(entity => entity.AccessLevel).HasMaxLength(50).IsRequired();
        resourcePermission.HasOne<Organization>().WithMany().HasForeignKey(entity => entity.OrganizationId);
        resourcePermission.HasOne(entity => entity.User).WithMany().HasForeignKey(entity => entity.UserId);
        resourcePermission.HasOne(entity => entity.Team).WithMany().HasForeignKey(entity => entity.TeamId);
        resourcePermission.HasIndex(entity => new { entity.OrganizationId, entity.ResourceType, entity.ResourceId });
    }
}
