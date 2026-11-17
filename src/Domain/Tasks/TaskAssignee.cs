using Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class TaskAssignee
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public int ProjectMemberId { get; set; }
        public ProjectMember ProjectMember { get; set; } = null!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
