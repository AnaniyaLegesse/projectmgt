using Domain.Organizations;
using Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class ProjectBaseline
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public int CreatedById { get; set; } 
        public User CreatedBy { get; set; } = null!;
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
    }
}
