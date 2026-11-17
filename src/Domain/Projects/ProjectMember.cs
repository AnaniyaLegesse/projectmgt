using Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public class ProjectMember
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        
        public Project Project { get; set; } = null!;
       
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Role { get; set; } = string.Empty;
   
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }

}
