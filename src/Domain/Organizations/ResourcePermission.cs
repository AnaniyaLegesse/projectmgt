namespace Domain.Organizations
{
    public class ResourcePermission
    {
        private ResourcePermission()
        {
        }

        public ResourcePermission(
            int organizationId,
            string resourceType,
            int resourceId,
            string accessLevel,
            int? userId,
            int? teamId)
        {
            if ((userId.HasValue && teamId.HasValue) || (!userId.HasValue && !teamId.HasValue))
            {
                throw new ArgumentException("A permission must target exactly one user or one team.");
            }

            if (organizationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(organizationId));
            }

            if (resourceId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(resourceId));
            }

            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            if (teamId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(teamId));
            }

            ArgumentException.ThrowIfNullOrWhiteSpace(resourceType);
            ArgumentException.ThrowIfNullOrWhiteSpace(accessLevel);

            OrganizationId = organizationId;
            ResourceType = resourceType;
            ResourceId = resourceId;
            AccessLevel = accessLevel;
            UserId = userId;
            TeamId = teamId;
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ResourceType { get; set; } = string.Empty;
        public int ResourceId { get; set; }
        public string AccessLevel { get; set; } = string.Empty;
        public int? UserId { get; private set; }
        public int? TeamId { get; private set; }
        public User? User { get; private set; }
        public Team? Team { get; private set; }
    }
}
