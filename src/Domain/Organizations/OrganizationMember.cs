using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class OrganizationMember
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string? Status { get; set; } 
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public Organization Organization { get; set; } = null!;
    }
}
