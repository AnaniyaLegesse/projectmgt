using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class ResourcePermission
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ResourceType { get; set; } = string.Empty;

        public int ResourceId { get; set; }
        public string AccessLevel { get; set; } = string.Empty;
  
        public int UserId { get; set; }
        public int TeamId { get; set; }

        public User User { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
