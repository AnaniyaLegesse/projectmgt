using Domain.common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectTask: BaseDomainEntity
    {

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Todo;
        public int Priority { get; set; } 

        // Foreign Key to Project
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        // Foreign Key to User (Assignee)
        public int? AssigneeId { get; set; }
        public User? Assignee { get; set; }

        // Relationship: One Task has many Comments
        public List<Comment> Comments { get; set; } = new();
    }
}
