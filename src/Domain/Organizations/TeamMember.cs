using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class TeamMember
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? Status { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Team Team { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
