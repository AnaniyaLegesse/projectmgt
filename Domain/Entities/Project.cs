using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project : BaseDomainEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }

        // Relationship: One Project has many Tasks
        public List<ProjectTask> Tasks { get; set; } = new();
    }
}
