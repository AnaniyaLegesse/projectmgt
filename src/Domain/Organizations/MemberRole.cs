using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class MemberRole
    {
        public int Id { get; set; }
        public int OrganizationMemberId { get; set; }
        public int RoleId { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;


        public OrganizationMember OrganizationMember { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
